using System;
namespace Identity.API.Providers
{
    public interface IWechatAuthProvider : IExternalAuthProvider
    {
        Provider Provider { get; }
    }
}
