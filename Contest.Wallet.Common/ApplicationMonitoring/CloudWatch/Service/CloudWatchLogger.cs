using Consent.Common.ApplicationMonitoring.CloudWatch.Abstraction;
using NLog;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Consent.Common.ApplicationMonitoring.CloudWatch.Service
{
    public class CloudWatchLogger<T> : ICloudWatchLogger<T> where T : class
    {
        #region Private Variables
        private readonly CloudWatchService _cwService;
        private readonly ICloudWatchConfiguration _config;
        private static string _logStream;
        private static string _nextSeqToken { get; set; }
        #endregion

        #region Constructor
        public CloudWatchLogger(ICloudWatchConfiguration config)
        {
            _config = config;
            _cwService = new CloudWatchService(_config);
        }
        #endregion

        public async Task Configure()
        {
            _logStream = GetLogStreamName();

            bool created = false;
            created = await _cwService.CreateLogGroupIfNotExists();
            created = await _cwService.CreateLogStreamIfNotExists(_logStream);

            if (created)
            {
                _nextSeqToken = string.Empty;
            }
        }

        public async Task Log(string message, LogLevel level)
        {
            if (level == LogLevel.Error)
                await LogError(message);

            if (level == LogLevel.Info)
                await LogInfo(message);

            if (level == LogLevel.Debug)
                await LogDebug(message);
        }

        public async Task LogInfo(string message)
        {
            _logStream = _logStream.Substring(_logStream.LastIndexOf("-") + 1) == DateTime.Now.ToShortTimeString()
                ? _logStream
                : GetLogStreamName();

            message = $"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.ffff")}|INFO|{typeof(T).FullName}|{message}";
            string token = await _cwService.Log(message, _logStream, _nextSeqToken);
            _nextSeqToken = token;
        }

        public async Task LogWarning(string message)
        {
            _logStream = _logStream.Substring(_logStream.LastIndexOf("-") + 1) == DateTime.Now.ToShortTimeString()
                ? _logStream
                : GetLogStreamName();

            message = $"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.ffff")}|WARNING|{typeof(T).FullName}|{message}";
            string token = await _cwService.Log(message, _logStream, _nextSeqToken);
            _nextSeqToken = token;
        }

        public async Task LogDebug(string message)
        {
            _logStream = _logStream.Substring(_logStream.LastIndexOf("-") + 1) == DateTime.Now.ToShortTimeString()
                ? _logStream
                : GetLogStreamName();

            message = $"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.ffff")}|DEBUG|{typeof(T).FullName}|{message}";
            string token = await _cwService.Log(message, _logStream, _nextSeqToken);
            _nextSeqToken = token;
        }

        public async Task LogError(Exception ex)
        {
            _logStream = _logStream.Substring(_logStream.LastIndexOf("-") + 1) == DateTime.Now.ToShortTimeString()
                ? _logStream
                : GetLogStreamName();

            string message = $"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.ffff")}|ERROR|{typeof(T).FullName}|{ex.StackTrace}";
            string token = await _cwService.Log(message, _logStream, _nextSeqToken);
            _nextSeqToken = token;
        }

        public async Task LogError(string err)
        {
            string message = $"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.ffff")}|ERROR|{typeof(T).FullName}|{err}";
            _logStream = _logStream.Substring(_logStream.LastIndexOf("-") + 1) == DateTime.Now.ToShortTimeString()
                ? _logStream
                : GetLogStreamName();
            string token = await _cwService.Log(message, _logStream, _nextSeqToken);
            _nextSeqToken = token;
        }

        private string GetLogStreamName()
        {
            return Assembly.GetEntryAssembly().GetName().Name
                    + "-" + DateTime.Now.ToString("yyyy-MM-dd");
        }
    }
}
