using System;
namespace SaaSEqt.Common.Domain.Model
{
    public interface IUnitOfWork : IDisposable
    {
        bool Commit();
    }
}
