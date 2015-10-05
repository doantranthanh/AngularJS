using System.Net.Http;
using System.Runtime.Remoting.Services;
using System.Web.Http.Dependencies;
using HomeCinema.Services.Abstract;

namespace HomeCinema.Web.Infrastructure.Extensions
{
    public static class RequestMessageExtension
    {
        internal static IMembershipService GetMembershipService(this HttpRequestMessage request)
        {
            return request.GetService<IMembershipService>();
        }

        private static TService GetService<TService>(this HttpRequestMessage request)
        {
            IDependencyScope dependencyScope = request.GetDependencyScope();

            TService service = (TService) dependencyScope.GetService(typeof (TService));

            return service;
        }
         
    }
}