using Contest.Wallet.Common.ApplicationMonitoring.Abstract;
using Contest.Wallet.Common.Firebase.Abstract;
using Contest.Wallet.Common.Firebase.Models;
using FirebaseAdmin.Messaging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contest.Wallet.Common.Firebase
{
    public class FirebaseService : IFirebaseService
    {
        #region Private Variables
        private readonly FirebaseMessaging _messaging;
        private readonly ILogger<FirebaseService> _logger;
        #endregion

        #region Constructor
        public FirebaseService(FirebaseMessaging messaging,
            ILogger<FirebaseService> logger)
        {
            _messaging = messaging ??
                throw new ArgumentNullException(nameof(messaging));
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
        }
        #endregion

        #region Public Methods
        public async Task SendNotification(FirebaseMessage message)
        {
            try
            {
                await _messaging.SendAsync(CreateNotification(message));
                _logger.LogInfo("Message notification {0} is sent successfully.", JsonConvert.SerializeObject(message));
            }
            catch (Exception ex)
            {
                _logger.LogError("Send Notification {0} is failed, Exception: {1}", JsonConvert.SerializeObject(message), ex.Message);
            }

        }
        #endregion

        #region Private Methods
        private Message CreateNotification(FirebaseMessage message)
        {
            return new Message()
            {
                Topic = message.Topic,
                Notification = new Notification()
                {
                    Body = message.Body,
                    Title = message.Title
                },
                Data = (IReadOnlyDictionary<string, string>)message.Data
            };
        }
        #endregion
    }
}