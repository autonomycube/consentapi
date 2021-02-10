using AutoMapper;
using Consent.Common.ApplicationMonitoring.Abstract;
using Consent.Api.Auth.Data.Repositories.Abstract;
using Consent.Api.Auth.Services.Abstract;
using System;
using System.Threading.Tasks;

namespace Consent.Api.Auth.Services
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
