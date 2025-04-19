using System;
using Identity.API.Models;

namespace Identity.API.Providers
{
    public interface IWechatAuthProvider : IExternalAuthProvider
    {
        Provider Provider { get; }
    }
}
