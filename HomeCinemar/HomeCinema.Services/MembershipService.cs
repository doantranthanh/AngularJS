using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using HomeCinema.Data.Infrastructure;
using HomeCinema.Data.Repositories;
using HomeCinema.Data.Extensions;
using HomeCinema.Entities;
using HomeCinema.Services.Abstract;
using HomeCinema.Services.Utilities;

namespace HomeCinema.Services
{
    public class MembershipService : IMembershipService
    {
        #region Variables

        private readonly IEntityBaseRepository<User> _userRepository;
        private readonly IEntityBaseRepository<Role> _roleRepository; 
        private readonly IEntityBaseRepository<UserRole> _userRoleRepository; 
        private readonly IEncryptionService _encryptionService; 
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        public MembershipService(IEntityBaseRepository<User> userRepository, IEntityBaseRepository<Role> roleRepository,
            IEntityBaseRepository<UserRole> userRoleRepository, IEncryptionService encryptionService,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository; 
            _userRoleRepository = userRoleRepository;
            _encryptionService = encryptionService; 
            _unitOfWork = unitOfWork;
        }


        #region Helper Methods

        private void AddUserToRole(User user, int roleId)
        {
            var role = _roleRepository.GetSingle(roleId);
            if(role == null)
                throw new ApplicationException("Role doesn't exist.");

            var userRole = new UserRole()
            {
                RoleId = role.ID,
                UserId = user.ID
            };

            _userRoleRepository.Add(userRole);
        }

        private bool IsPasswordValid(User user, string password)
        {
            return string.Equals(_encryptionService.EncryptPassword(password, user.Salt), user.HashedPassword);
        }

        private bool IsUserValid(User user, string password)
        {
            if (IsPasswordValid(user, password))
            {
                return !user.IsLocked;
            }

            return false;
        }
        #endregion

        #region IMembershipService Implementation

        public MembershipContext ValidateUser(string username, string password)
        {
            var membershipCtx = new MembershipContext();

            var user = _userRepository.GetSingleByUsername(username);
            if (user != null && IsUserValid(user, password))
            {
                var userRoles = GetUserRoles(user.UserName);
                membershipCtx.User = user;

                var identity = new GenericIdentity(user.UserName);

                membershipCtx.Principal = new GenericPrincipal(identity,userRoles.Select(x => x.Name).ToArray());
            }

            return membershipCtx;
        }

        public User CreateUser(string username, string email, string password, int[] roles)
        {
           
            var existingUser = _userRepository.GetSingleByUsername(username);
            if (existingUser != null)
            {
                throw new Exception("Username is already in use");
            }

            var passwordSalt = _encryptionService.CreateSalt();

            var user = new User
            {
                UserName = username,
                Salt = passwordSalt,
                Email = email,
                IsLocked = false,
                HashedPassword = _encryptionService.EncryptPassword(password,passwordSalt),
                DateCreated = DateTime.Now
            };

            _userRepository.Add(user);
            _unitOfWork.Commit();

            if (roles != null || roles.Length > 0)
            {
                foreach (var role in roles)
                {
                    AddUserToRole(user, role);
                }
            }

            _unitOfWork.Commit();

            return user;
        }

        public User GetUser(int userID)
        {
            return _userRepository.GetSingle(userID);
        }

        public List<Role> GetUserRoles(string username)
        {
            var result = new List<Role>();

            var existingUser = _userRepository.GetSingleByUsername(username);

            if (existingUser != null)
            {
                result.AddRange(existingUser.UserRoles.Select(userRole => userRole.Role));
            }

            return result.Distinct().ToList();
        }

        #endregion

       
    }
}
