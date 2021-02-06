using AutoMapper;
using Contest.Wallet.Common.ApplicationMonitoring.Abstract;
using Contest.Wallet.Api.Notification.Data.Repositories.Abstract;
using Contest.Wallet.Api.Notification.Services.Abstract;
using System;
using System.Threading.Tasks;

namespace Contest.Wallet.Api.Notification.Services
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
