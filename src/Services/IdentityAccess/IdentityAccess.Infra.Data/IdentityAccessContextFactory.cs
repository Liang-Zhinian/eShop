using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SaaSEqt.IdentityAccess.Infra.Data.Context;

namespace SaaSEqt.IdentityAccess.Infra.Data
{
    public class IdentityAccessContextFactory : IDesignTimeDbContextFactory<IdentityAccessDbContext>
    {
        public IdentityAccessDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<IdentityAccessDbContext>();
            optionsBuilder.UseMySql("Server=localhost;database=book2business;uid=root;pwd=P@ssword;charset=utf8;port=3306;SslMode=None");

            return new IdentityAccessDbContext(optionsBuilder.Options);
        }
    }
}
