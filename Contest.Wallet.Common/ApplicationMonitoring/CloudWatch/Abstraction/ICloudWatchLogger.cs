using NLog;
using System;
using System.Threading.Tasks;

namespace Consent.Common.ApplicationMonitoring.CloudWatch.Abstraction
{
    public interface ICloudWatchLogger<T> where T : class
    {
        Task Configure();
        Task Log(string message, LogLevel level);
        Task LogInfo(string message);
        Task LogWarning(string message);
        Task LogError(string message);
        Task LogError(Exception ex);
        Task LogDebug(string message);
    }
}