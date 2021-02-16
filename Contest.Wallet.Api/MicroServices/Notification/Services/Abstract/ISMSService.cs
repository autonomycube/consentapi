﻿using Consent.Api.Notification.API.v1.DTO.Request;
using System.Threading.Tasks;

namespace Consent.Api.Notification.Services.Abstract
{
    public interface ISMSService
    {
        Task<string> SendOTP(CreateSmsOtpRequest request);
        Task<bool> VerifyOTP(VerifySmsOtpRequest request);
    }
}
