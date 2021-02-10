using AutoMapper;
using Consent.Common.ApplicationMonitoring.Abstract;
using Consent.Api.Notification.Data.Repositories.Abstract;
using Consent.Api.Notification.Services.Abstract;
using System;
using System.Threading.Tasks;

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

        #region CRUD - R

        public async Task<string[]> Get()
        {
            return await Task.FromResult(new string[] { "Test Method" });
        }

        #endregion

    }
}
