using Amazon.CloudWatchLogs;
using Amazon.CloudWatchLogs.Model;
using Contest.Wallet.Common.ApplicationMonitoring.CloudWatch.Abstraction;
using System;
using System.Threading.Tasks;

namespace Contest.Wallet.Common.ApplicationMonitoring.CloudWatch.Service
{
    internal class CloudWatchService
    {
        #region Private Variables
        private readonly ICloudWatchConfiguration _config;
        #endregion

        #region Constructor
        public CloudWatchService(ICloudWatchConfiguration config)
        {
            _config = config;
        }
        #endregion

        /// <summary>
        /// Creates log group in cloud watch if it does not exist
        /// </summary>
        /// <param name="logGroupName">name of the log group to be created if not exists.</param>
        /// <returns></returns>
        internal async Task<bool> CreateLogGroupIfNotExists()
        {
            try
            {
                if (string.IsNullOrEmpty(_config.GroupName))
                    throw new ArgumentNullException("Log group name is required");

                using (var cwClient =
                    new AmazonCloudWatchLogsClient(_config.AccessKey, _config.SecretKey, _config.Endpoint))
                {
                    CreateLogGroupRequest createLogGroupRequest = new CreateLogGroupRequest()
                    {
                        LogGroupName = _config.GroupName
                    };

                    var createLogGroupResponse = await cwClient.CreateLogGroupAsync(createLogGroupRequest);

                    if (createLogGroupResponse.HttpStatusCode == System.Net.HttpStatusCode.OK)
                        return true;

                    return false;
                }
            }
            catch (ResourceAlreadyExistsException)
            {
                return false;
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Creates log stream in log group if it does not exist.
        /// </summary>
        /// <param name="logGroupName">log group name.</param>
        /// <param name="logStreamName">name of the log stream to be created if not exists.</param>
        /// <returns></returns>
        internal async Task<bool> CreateLogStreamIfNotExists(string logStreamName)
        {
            try
            {
                if (_config.TraceLog)
                {
                    using (var cwClient = new AmazonCloudWatchLogsClient(
                        _config.AccessKey, _config.SecretKey, _config.Endpoint))
                    {
                        var createLogStreamRequest = new CreateLogStreamRequest()
                        {
                            LogGroupName = _config.GroupName,
                            LogStreamName = logStreamName
                        };
                        var createLogStreamResonse = await cwClient.CreateLogStreamAsync(createLogStreamRequest);

                        if (createLogStreamResonse.HttpStatusCode == System.Net.HttpStatusCode.OK)
                            return true;

                        return false;
                    }
                }

                return true;
            }
            catch (ResourceAlreadyExistsException)
            {
                return false;
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Logs message to CloudWatch.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="logStreamName"></param>
        /// <param name="sequenceToken"></param>
        /// <returns></returns>
        internal async Task<string> Log(string message, string logStreamName, string sequenceToken)
        {
            try
            {
                if (_config.TraceLog)
                {
                    using (var cwClient = new AmazonCloudWatchLogsClient(
                        _config.AccessKey, _config.SecretKey, _config.Endpoint))
                    {
                        var putLogEventsRequest = new PutLogEventsRequest()
                        {
                            LogGroupName = _config.GroupName,
                            LogStreamName = logStreamName,
                            LogEvents = new System.Collections.Generic.List<InputLogEvent>(),
                            SequenceToken = sequenceToken
                        };

                        var logEvent = new InputLogEvent()
                        {
                            Message = message,
                            Timestamp = DateTime.Now
                        };

                        putLogEventsRequest.LogEvents.Add(logEvent);

                        var putLogEventsResponse = await cwClient.PutLogEventsAsync(putLogEventsRequest);

                        if (putLogEventsResponse.HttpStatusCode == System.Net.HttpStatusCode.OK)
                            return putLogEventsResponse.NextSequenceToken;

                        throw new ArgumentException("Error writing logs - ", putLogEventsResponse.HttpStatusCode.ToString());
                    }
                }

                return string.Empty;
            }
            catch (InvalidSequenceTokenException ex)
            {
                int index = ex.Message.LastIndexOf(":") + 2;
                string seqToken = ex.Message.Substring(index).ToLower() == "null" ? null : ex.Message.Substring(index);
                return await Log(message, logStreamName, seqToken);
            }
            catch (DataAlreadyAcceptedException ex)
            {
                int index = ex.Message.LastIndexOf(":") + 2;
                string seqToken = ex.Message.Substring(index).ToLower() == "null" ? null : ex.Message.Substring(index);
                return await Log(message, logStreamName, seqToken);
            }
            catch (ResourceNotFoundException)
            {
                await CreateLogGroupIfNotExists();
                await CreateLogStreamIfNotExists(logStreamName);

                return await Log(message, logStreamName, sequenceToken);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
