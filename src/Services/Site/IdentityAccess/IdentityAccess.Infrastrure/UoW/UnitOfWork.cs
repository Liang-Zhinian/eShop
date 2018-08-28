using System;
using SaaSEqt.Common.Domain.Model;
using SaaSEqt.IdentityAccess.Infrastructure.Context;

namespace SaaSEqt.IdentityAccess.Infrastructure.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IdentityAccessDbContext _context;

        public UnitOfWork(IdentityAccessDbContext context)
        {
            _context = context;
        }

        public bool Commit()
        {
            return _context.SaveChanges() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
