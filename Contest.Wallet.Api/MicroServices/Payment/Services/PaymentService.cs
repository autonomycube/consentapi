﻿using AutoMapper;
using Contest.Wallet.Api.Payment.Services.Abstract;
using Contest.Wallet.Api.Tenant.Data.Repositories.Abstract;
using Contest.Wallet.Common.ApplicationMonitoring.Abstract;
using System;
using System.Threading.Tasks;

namespace Contest.Wallet.Api.Payment.Services
{
    public class PaymentService : IPaymentService
    {
        #region Private Variables

        private readonly IIssuerRepository _testRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<PaymentService> _logger;

        #endregion

        #region Constructor

        public PaymentService(IIssuerRepository testRepository,
            IMapper mapper,
            ILogger<PaymentService> logger)
        {
            _testRepository = testRepository
                ?? throw new ArgumentNullException(nameof(testRepository));
            _mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger
                ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion

        #region CRUD - R

        public async Task<string[]> Get()
        {
            return await Task.FromResult(new string[] { "Test Method" });
        }

        #endregion

    }
}