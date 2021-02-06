using Microsoft.Extensions.Configuration;

namespace Contest.Wallet.Common.Configuration
{
    public interface IAppSetting
    {
        string Get(string key);
        IConfigurationSection GetSection(string section);
    }
}
