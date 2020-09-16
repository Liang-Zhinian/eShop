// Author: 	sprite
// On:		2020/5/6
using System;
using Eva.BuildingBlocks.IntegrationEventLogEF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Ordering.API.Infrastructure
{
    public class IntegrationEventLogContextDesignFactory : IDesignTimeDbContextFactory<IntegrationEventLogContext>
    {
        public IntegrationEventLogContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<IntegrationEventLogContext>()
                .UseMySql("Server=127.0.0.1;database=Eva_eShop_Services_CatalogDb;uid=root;pwd=P@ssword;charset=utf8;port=3306;SslMode=None");

            return new IntegrationEventLogContext(optionsBuilder.Options);
        }
    }
}
