using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using HomeCinema.Data;
using HomeCinema.Data.Configuration;
using HomeCinema.Data.Infrastructure;
using HomeCinema.Data.Repositories;
using HomeCinema.Services;
using HomeCinema.Services.Abstract;

namespace HomeCinema.Web.App_Start
{
    public class AutofacWebapiConfig
    {
        public static IContainer Container { get; set; }

        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterService(new ContainerBuilder()));
        }

        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterService(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // EF HomeCinema Context
            builder.RegisterType<HomeCinemaContext>().As<DbContext>().InstancePerRequest();

            builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerRequest();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();

            builder.RegisterGeneric(typeof (EntityBaseConfiguration<>))
                .As(typeof (IEntityBaseRepository<>))
                .InstancePerRequest();

            // Service
            builder.RegisterType<EncryptionService>().As<IEncryptionService>().InstancePerRequest();

            builder.RegisterType<MembershipService>().As<IMembershipService>().InstancePerRequest();

            Container = builder.Build();
            
            return Container;
        }
    }
}