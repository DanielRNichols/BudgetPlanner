using BudgetPlannerApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerApi.Services
{
    public class ConsoleLoggerService : ILoggerService
    {
        public void LogDebug(string message)
        {
            Console.WriteLine($"Debug: {message}");
        }

        public void LogError(string message)
        {
            Console.WriteLine($"Error: {message}");
        }

        public void LogInfo(string message)
        {
            Console.WriteLine($"Info: {message}");
        }

        public void LogMessage(LoggerLevel level, string message)
        {
            switch (level)
            {
                case LoggerLevel.Debug:
                    LogDebug(message);
                    break;

                case LoggerLevel.Error:
                    LogError(message);
                    break;

                case LoggerLevel.Warn:
                    LogWarn(message);
                    break;

                default:
                    LogInfo(message);
                    break;
            }
        }
        
        public void LogServerError(string msg)
        {
            LogError($"Server Error: {msg}");
        }

        public void LogServerError(Exception e)
        {
            LogServerError($"{ e.Message} - {e.InnerException}");
        }

        public void LogWarn(string message)
        {
            Console.WriteLine($"Warn: {message}");
        }
    }
}
