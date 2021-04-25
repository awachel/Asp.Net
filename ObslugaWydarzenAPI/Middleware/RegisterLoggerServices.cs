using Logger.Middleware;
using Microsoft.Extensions.DependencyInjection;
using ObslugaWydarzenAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ObslugaWydarzenAPI.Middleware
{
    public static class RegisterLoggerServices
    {
        public static IServiceCollection AddLoggers(this IServiceCollection services)
        {
            services.AddLogger<EventsController>();
            return services;
        } 
    }
}
