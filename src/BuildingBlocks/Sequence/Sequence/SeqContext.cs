// Author: 	Liang Zhinian
// On:		2020/5/23
using System;
using Sequence.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Sequence
{
    public class SeqContext : DbContext
    {
        public SeqContext(DbContextOptions<SeqContext> options) : base(options)
        {
        }

        public DbSet<KeyAlloc> KeyAlloc { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new KeyAllocEntityTypeConfiguration());
        }
    }
}
