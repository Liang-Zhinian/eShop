using System;
using Microsoft.EntityFrameworkCore;
using SaaSEqt.IdentityAccess.Infra.Data.Mappings;
using System.ComponentModel.DataAnnotations;
using SaaSEqt.IdentityAccess.Domain.Access.Entities;
using SaaSEqt.IdentityAccess.Domain.Identity.Entities;
using SaaSEqt.IdentityAccess.Infra.Data.Mappings.Constants;

namespace SaaSEqt.IdentityAccess.Infra.Data.Context
{
    public class IdentityAccessDbContext : DbContext
    {
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<User> Staffs { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<GroupMember> GroupMembers { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Role> Roles { get; set; }

        public IdentityAccessDbContext(DbContextOptions<IdentityAccessDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.HasDefaultSchema(DbConstants.Schema);
            
            modelBuilder.ApplyConfiguration(new TenantMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new PersonMap());
            modelBuilder.ApplyConfiguration(new RoleMap());
            modelBuilder.ApplyConfiguration(new GroupMap());
            modelBuilder.ApplyConfiguration(new GroupMemberMap());

        }
    }
}
