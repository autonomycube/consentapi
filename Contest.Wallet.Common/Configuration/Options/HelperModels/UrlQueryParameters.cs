namespace Consent.Common.Configuration.Options.HelperModels
{
    public class UrlQueryParameters
    {
        const int maxPageSize = 50;
        private int _pageSize = 25;
        public int PageNumber { get; set; } = 1;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > maxPageSize) ? maxPageSize : value;
        }
        public bool IncludeCount { get; set; } = false;
    }
}
