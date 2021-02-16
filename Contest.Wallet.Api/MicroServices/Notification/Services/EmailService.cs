using AutoMapper;
using Consent.Common.ApplicationMonitoring.Abstract;
using Consent.Api.Notification.Data.Repositories.Abstract;
using Consent.Api.Notification.Services.Abstract;
using System;
using System.Threading.Tasks;
using Consent.Api.Notification.API.v1.DTO.Request;

namespace Consent.Api.Notification.Services
{
    public class EmailService : IEmailService
    {
        #region Private Variables

        private readonly IMapper _mapper;
        private readonly ILogger<EmailService> _logger;

        #endregion

        #region Constructor

        public EmailService(IMapper mapper,
            ILogger<EmailService> logger)
        {
            _mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger
                ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion

        #region CRUD - C

        public async Task<bool> SendEmail(CreateEmailRequest request)
        {
            return await Task.FromResult(true);
        }

        #endregion

    }
}
