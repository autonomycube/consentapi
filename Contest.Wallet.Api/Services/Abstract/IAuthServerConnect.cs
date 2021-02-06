using System.Threading.Tasks;

namespace Contest.Wallet.Api.Contracts
{
    public interface IAuthServerConnect
    {
        Task<string> RequestClientCredentialsTokenAsync();
    }
}
