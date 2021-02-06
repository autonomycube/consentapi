using Contest.Wallet.Common.ApplicationMonitoring.Abstract;
using Contest.Wallet.Common.ApplicationMonitoring.CloudWatch.Abstraction;
using NLog;
using System;

namespace Contest.Wallet.Common.ApplicationMonitoring
{
    public class Logger<T> : ILogger<T> where T : class
    {
        private ICloudWatchLogger<T> _cwLogger;

        public Logger(ICloudWatchLogger<T> cWLogger)
        {
            _cwLogger = cWLogger;
            _cwLogger.Configure();
        }

        public void LogInfo(object message)
        {
            _cwLogger.LogInfo(message.ToString());
        }

        public void LogInfo(string message, params object[] args)
        {
            if (args.Length > 0)
            {
                _cwLogger.LogInfo(string.Format(message, args));
            }
            else
            {
                _cwLogger.LogInfo(message);
            }
        }

        public void LogWarning(object message)
        {
            _cwLogger.LogWarning(message.ToString());
        }

        public void LogWarning(string message, params object[] args)
        {
            if (args.Length > 0)
            {
                _cwLogger.LogWarning(string.Format(message, args));
            }
            else
            {
                _cwLogger.LogWarning(message);
            }
        }

        public void LogError(object message)
        {
            _cwLogger.LogError(message.ToString());
        }

        public void LogError(string message, params object[] args)
        {
            if (args.Length > 0)
            {
                _cwLogger.LogError(string.Format(message, args));
            }
            else
            {
                _cwLogger.LogError(message);
            }
        }

        public void LogError(Exception exception)
        {
            _cwLogger.LogError(exception);
        }

        public void Log(object message, LogLevel level)
        {
            _cwLogger.Log(message.ToString(), level);
        }
    }
}