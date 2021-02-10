using Consent.Common.EventCommunication.Abstract;
using JustSaying.Messaging;
using JustSaying.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Consent.Common.EventCommunication
{
    public class Publisher : IPublisher
    {
        #region Private Variables
        private readonly JustSaying.Messaging.IMessagePublisher _publisher;
        private ILogger<Publisher> _logger;
        #endregion

        #region Constructor
        public Publisher(IMessagePublisher publisher, ILogger<Publisher> logger)
        {
            _publisher = publisher;
            _logger = logger;
        }
        #endregion

        #region Public Methods
        public async Task Publish(Message message)
        {
            try
            {
                await _publisher.PublishAsync(message);
                _logger.LogInformation("Message published successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to publish message.  Exception: " + ex.Message);
            }
        }
        #endregion
    }
}