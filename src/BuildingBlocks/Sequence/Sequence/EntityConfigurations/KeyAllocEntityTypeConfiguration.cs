// Author: 	Liang Zhinian
// On:		2020/5/23
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MySql.Data.EntityFrameworkCore.Extensions;

namespace Sequence.EntityConfigurations
{
    public class KeyAllocEntityTypeConfiguration
        : IEntityTypeConfiguration<KeyAlloc>
    {
        public void Configure(EntityTypeBuilder<KeyAlloc> builder)
        {
            builder.ToTable("tbKeyAlloc");
            builder.HasKey(o => o.Id);
            builder.Property(o=>o.Id)
               .UseMySQLAutoIncrementColumn("key_alloc_hilo")
               .IsRequired();
            builder.Property(o => o.SequenceName).IsRequired();
            builder.Property(o => o.IncrementSize).IsRequired();
            builder.Property(o => o.MinValue).IsRequired();
            builder.Property(o => o.MaxValue).IsRequired();
            builder.Property(o => o.NextValue).IsRequired();

            builder.HasAlternateKey(o => o.SequenceName);
        }
    }
}
