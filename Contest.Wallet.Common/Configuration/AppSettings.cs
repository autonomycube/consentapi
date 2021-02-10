using Microsoft.Extensions.Configuration;
using System.IO;

namespace Consent.Common.Configuration
{
    public class AppSettings : IAppSetting
    {
        protected IConfiguration Configuration;
        public AppSettings(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public string Get(string key)
        {
            return Configuration[key];
        }

        public IConfigurationSection GetSection(string section)
        {
            return Configuration.GetSection(section);
        }
    }
}
