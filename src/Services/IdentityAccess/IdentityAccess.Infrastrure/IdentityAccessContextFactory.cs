using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SaaSEqt.IdentityAccess.Infrastructure.Context;

namespace SaaSEqt.IdentityAccess.Infrastructure
{
    public class IdentityAccessContextFactory : IDesignTimeDbContextFactory<IdentityAccessDbContext>
    {
        public IdentityAccessDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<IdentityAccessDbContext>();
            optionsBuilder.UseMySql("Server=localhost;database=Book2_BusinessDb;uid=root;pwd=P@ssword;charset=utf8;port=3306;SslMode=None");

            return new IdentityAccessDbContext(optionsBuilder.Options);
        }
    }
}
