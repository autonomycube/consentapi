using NLog;
using System;

namespace Contest.Wallet.Common.ApplicationMonitoring.Abstract
{
    public interface ILogger<T> where T : class
    {
        void LogInfo(object message);
        void LogInfo(string message, params object[] args);
        void LogWarning(object message);
        void LogWarning(string message, params object[] args);
        void LogError(object message);
        void LogError(string message, params object[] args);
        void LogError(Exception exception);
        void Log(object message, LogLevel level);
    }
}
