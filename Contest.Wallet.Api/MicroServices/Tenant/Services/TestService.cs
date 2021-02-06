using AutoMapper;
using Contest.Wallet.Common.ApplicationMonitoring.Abstract;
using Contest.Wallet.Api.Tenant.Data.Repositories.Abstract;
using Contest.Wallet.Api.Tenant.Services.Abstract;
using System;
using System.Threading.Tasks;

namespace Contest.Wallet.Api.Tenant.Services
{
    public class TestService : ITestService
    {
        #region Private Variables

        private readonly IIssuerRepository _testRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<TestService> _logger;

        #endregion

        #region Constructor

        public TestService(IIssuerRepository testRepository,
            IMapper mapper,
            ILogger<TestService> logger)
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
