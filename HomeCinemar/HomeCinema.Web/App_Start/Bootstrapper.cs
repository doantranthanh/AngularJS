using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using AutoMapper;

namespace HomeCinema.Web.App_Start
{
    public class Bootstrapper
    {
        public static void Run()
        {
            // Configure Autoface
            AutofacWebapiConfig.Initialize(GlobalConfiguration.Configuration);

            // Configre AutoMapper
            //AutoMapperConfiguration.Configure();
        }
    }
}