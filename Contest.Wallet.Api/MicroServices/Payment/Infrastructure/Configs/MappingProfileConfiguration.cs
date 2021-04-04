using AutoMapper;
using Consent.Api.MicroServices.Payment.DTO.Response;
using Consent.Common.EnityFramework.Entities;

namespace Consent.Api.Payment.Infrastructure.Configs
{
    public class MappingProfileConfiguration : Profile
    {
        public MappingProfileConfiguration()
        {
            // Mapping DTO to Models

            // response
            CreateMap<TblPaymentTransactions, PaymentConfirmationResponse>();
        }
    }
}
