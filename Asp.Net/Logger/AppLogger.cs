﻿using Logger.DAL;
using Logger.Models;
using Logger.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public class AppLogger<T> : ILogger<T>
    {
        private readonly LoggerContext context;
        public AppLogger(string loggerConnectionString)
        {
            context = CreateDbService.CreateDb(loggerConnectionString);
        }

        public AppLogger(IConfiguration configuration)
        {
            context = CreateDbService.CreateDb(configuration.GetConnectionString("LoggerConnection"));
        }
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel == LogLevel.Information || logLevel == LogLevel.Error;
            
           
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }
            var message = formatter(state, exception);
            if (string.IsNullOrEmpty (message))
            {
                return;
            }
            try
            {
                context.Add(new Log
                {
                    ExecutingType = typeof(T)?.ToString(),
                    Exception = exception?.ToString(),
                    LogLevel = logLevel.ToString(),
                    Message = message,
                    Stacktrace = exception?.StackTrace,
                    Timestamp = DateTime.Now,
                    Username= "TODO" //TODO
                });
                context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
