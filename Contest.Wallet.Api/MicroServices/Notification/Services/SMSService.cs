﻿using AutoMapper;
using Consent.Common.ApplicationMonitoring.Abstract;
using Consent.Api.Notification.Data.Repositories.Abstract;
using Consent.Api.Notification.Services.Abstract;
using System;
using System.Threading.Tasks;
using Consent.Api.Notification.API.v1.DTO.Request;

namespace Consent.Api.Notification.Services
{
    public class SMSService : ISMSService
    {
        #region Private Variables

        private readonly ISMSRepository _testRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<SMSService> _logger;

        #endregion

        #region Constructor

        public SMSService(ISMSRepository testRepository,
            IMapper mapper,
            ILogger<SMSService> logger)
        {
            _testRepository = testRepository
                ?? throw new ArgumentNullException(nameof(testRepository));
            _mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger
                ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion

        #region CRUD - C

        public async Task<string> SendOTP(CreateSmsOtpRequest request)
        {
            return await Task.FromResult(string.Empty);
        }

        public async Task<bool> VerifyOTP(VerifySmsOtpRequest request)
        {
            return await Task.FromResult(true);
        }

        #endregion

    }
}
