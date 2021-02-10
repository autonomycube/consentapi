using Microsoft.Extensions.Configuration;

namespace Consent.Common.Configuration
{
    public interface IAppSetting
    {
        string Get(string key);
        IConfigurationSection GetSection(string section);
    }
}
