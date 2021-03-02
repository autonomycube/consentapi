namespace Consent.Api.Tenant.DTO.Response
{
    public class HolderEmailAddressesResponse
    {
        public string Email { get; set; }
        public EnumHolderEmailAddressStatus Status { get; set; }
    }

    public enum EnumHolderEmailAddressStatus
    {
        Valid = 0,
        AlreadyRegistered = 1,
        AlreadySentInvitation = 2,
        InvitationSent = 3
    }
}
