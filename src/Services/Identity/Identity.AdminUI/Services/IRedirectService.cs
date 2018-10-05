namespace SaaSEqt.eShop.Services.IdentityManagement.API.Services
{
    public interface IRedirectService
    {
        string ExtractRedirectUriFromReturnUrl(string url);
    }
}
