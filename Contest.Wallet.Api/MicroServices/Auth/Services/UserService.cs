using Consent.Api.Auth.Data.Repositories.Abstract;
using Consent.Api.Auth.Services.Abstract;
using Consent.Common.EnityFramework.Entities.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace Consent.Api.Auth.Services
{
    public class UserService : IUserService
    {
        #region Private Variables

        private readonly IUserRepository _userRepository;

        #endregion

        #region Constructor

        public UserService(IUserRepository userRepository)
        {
            _userRepository=userRepository;
        }

        #endregion

        #region Public Methods

        public UserIdentity GetById(string id)
        {
           return  _userRepository.GetById(id);
        }

        public async Task<UserIdentity> GetByPhoneNumber(string phoneNumber)
        {
            return (await _userRepository.FindBy(u => u.PhoneNumber == phoneNumber)).FirstOrDefault();
        }

        public async Task<UserIdentity> GetByEmail(string email)
        {
            return (await _userRepository.FindBy(u => u.Email == email)).FirstOrDefault();
        }

        public async Task Update(UserIdentity entity)
        {
            await _userRepository.Update(entity);
        }

        #endregion
    }
}