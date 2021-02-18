using Consent.Common.ApplicationMonitoring.Abstract;
using Consent.Common.Constants;
using Consent.Common.Helpers.Abstract;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using Consent.Common.EnityFramework.Constants;

namespace Consent.Common.Helpers
{
    public class BaseAuthHelper : IBaseAuthHelper
    {
        #region Private Variables

        private readonly IHttpContextAccessor _httpContext;
        private readonly ILogger<BaseAuthHelper> _logger;

        #endregion

        #region Constructor

        public BaseAuthHelper(IHttpContextAccessor httpContext,
            ILogger<BaseAuthHelper> logger)
        {
            _httpContext = httpContext
                ?? throw new ArgumentNullException(nameof(httpContext));
            _logger = logger
                ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion

        #region Public Methods

        public string GetUserId()
        {
            //return GetData(ProfileConstants.UserId);
            return ConsentConsts.UserId;
        }

        public string GetTenantId()
        {
            //return GetData(ProfileConstants.TenantId);
            return ConsentConsts.TenantId;
        }

        #endregion

        #region Private Methods

        private string GetData(string key)
        {
            string data = _httpContext.HttpContext.Request.Headers[key];
            if (string.IsNullOrEmpty(data))
            {
                data = _httpContext.HttpContext.User?.Claims.FirstOrDefault(a => a.Type == key)?.Value;
            }

            if (string.IsNullOrEmpty(data) && key == ProfileConstants.TenantId)
            {
                _logger.LogWarning("{0} is found null in request.", key);
            }

            return data;
        }

        #endregion
    }
}