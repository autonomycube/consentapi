using System.ComponentModel;

namespace Consent.Common.Configuration.Options.HelperModels
{
    public class CachingOptionsModel
    {
        [DefaultValue(5)]
        public int ExpiredInMinute { get; set; }
        [DefaultValue(1)]
        public int SlidingInMinute { get; set; }
        [DefaultValue(10)]
        public int RetryInSecond { get; set; }
    }
}
