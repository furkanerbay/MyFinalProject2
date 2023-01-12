using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.IoC
{
    public static class ServiceTool
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public static IServiceCollection Create(IServiceCollection services) //.NET'in servislerini kullanarak onları al
        {
            ServiceProvider = services.BuildServiceProvider(); //Onları build et.
            return services;
        }
    }
}
