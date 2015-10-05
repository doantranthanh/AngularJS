using System.Linq;
using HomeCinema.Data.Repositories;
using HomeCinema.Entities;

namespace HomeCinema.Data.Extensions
{
    public static class UserExtension
    {
        public static User GetSingleByUsername(this IEntityBaseRepository<User> userRepositiory, string username)
        {
            return userRepositiory.GetAll().FirstOrDefault(x => x.UserName == username);
        }
    }
}
