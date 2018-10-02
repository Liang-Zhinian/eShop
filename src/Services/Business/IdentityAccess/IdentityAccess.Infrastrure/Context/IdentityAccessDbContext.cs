using System;
using Microsoft.EntityFrameworkCore;
using SaaSEqt.IdentityAccess.Infrastructure.Mappings;
using System.ComponentModel.DataAnnotations;
using SaaSEqt.IdentityAccess.Domain.Model.Access.Entities;
using SaaSEqt.IdentityAccess.Domain.Model.Identity.Entities;
using SaaSEqt.IdentityAccess.Infrastructure.Mappings.Constants;

namespace SaaSEqt.IdentityAccess.Infrastructure.Context
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
