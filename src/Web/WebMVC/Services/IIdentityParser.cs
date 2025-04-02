namespace Eva.eShop.WebMVC.Services;

public interface IIdentityParser<T>
{
    T Parse(IPrincipal principal);
}
