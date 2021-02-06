using Contest.Wallet.Common.Configuration.Extensions.Extensions;

namespace Contest.Wallet.Common.Configuration.Options
{
    public class CachingOptions : OptionsBase
    {
        public CachingOptions(IAppSetting appSettings) : base(appSettings)
        {
        }

        public int ExpiredInMinute => GetInt().Default(5);
        public int SlidingInMinute => GetInt().Default(1);
        public int RetryInSecond => GetInt().Default(10);
        public bool NeverRemove { get; set; }
    }
}
