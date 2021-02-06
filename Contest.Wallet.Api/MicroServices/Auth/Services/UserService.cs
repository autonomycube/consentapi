using AutoMapper;
using Contest.Wallet.Common.ApplicationMonitoring.Abstract;
using Contest.Wallet.Api.Auth.Data.Repositories.Abstract;
using Contest.Wallet.Api.Auth.Services.Abstract;
using System;
using System.Threading.Tasks;

namespace Contest.Wallet.Api.Auth.Services
{
    public class UserService : IUserService
    {
        #region Private Variables

        private readonly IUserRepository _testRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;

        #endregion

        #region Constructor

        public UserService(IUserRepository testRepository,
            IMapper mapper,
            ILogger<UserService> logger)
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
