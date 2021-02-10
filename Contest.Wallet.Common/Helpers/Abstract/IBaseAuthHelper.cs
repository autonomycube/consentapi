
namespace Consent.Common.Helpers.Abstract
{
    public interface IBaseAuthHelper
    {
        string GetBlueNumber();
        string GetTenantId();
        string GetIssuer();
    }
}
