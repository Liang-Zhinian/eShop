﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using SaaSEqt.IdentityAccess.Infra.Data.Context;
using System;

namespace SaaSEqt.IdentityAccess.Infra.Data.Migrations
{
    [DbContext(typeof(IdentityAccessDbContext))]
    [Migration("20180706185039_first")]
    partial class first
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("book2business")
                .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026");

            modelBuilder.Entity("SaaSEqt.IdentityAccess.Domain.Entities.Group", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("MySQL:Collation", "utf8_general_ci");

                    b.Property<string>("Description")
                        .HasColumnType("varchar(2000)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("TenantId_Id")
                        .IsRequired()
                        .HasColumnName("TenantId_Id")
                        .HasColumnType("varchar(36)")
                        .HasAnnotation("MySQL:Collation", "utf8_general_ci");

                    b.HasKey("Id");

                    b.HasIndex("TenantId_Id");

                    b.ToTable("Group");

                    b.HasAnnotation("MySQL:Charset", "utf8");

                    b.HasAnnotation("MySQL:Collation", "utf8_general_ci");
                });

            modelBuilder.Entity("SaaSEqt.IdentityAccess.Domain.Entities.GroupMember", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)")
                        .HasAnnotation("MySQL:Collation", "utf8_general_ci");

                    b.Property<Guid>("GroupId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("TenantId_Id");

                    b.Property<int>("TypeValue")
                        .HasColumnName("Type");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("TenantId_Id");

                    b.ToTable("GroupMember");

                    b.HasAnnotation("MySQL:Charset", "utf8");

                    b.HasAnnotation("MySQL:Collation", "utf8_general_ci");
                });

            modelBuilder.Entity("SaaSEqt.IdentityAccess.Domain.Entities.Person", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("MySQL:Collation", "utf8_general_ci");

                    b.Property<string>("TenantId_Id")
                        .IsRequired()
                        .HasColumnName("TenantId_Id")
                        .HasColumnType("varchar(36)")
                        .HasAnnotation("MySQL:Collation", "utf8_general_ci");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("TenantId_Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Person");

                    b.HasAnnotation("MySQL:Charset", "utf8");

                    b.HasAnnotation("MySQL:Collation", "utf8_general_ci");
                });

            modelBuilder.Entity("SaaSEqt.IdentityAccess.Domain.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("MySQL:Collation", "utf8_general_ci");

                    b.Property<string>("Description")
                        .HasColumnType("varchar(2000)");

                    b.Property<Guid>("GroupId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<bool>("SupportsNesting");

                    b.Property<string>("TenantId_Id")
                        .IsRequired()
                        .HasColumnName("TenantId_Id")
                        .HasColumnType("varchar(36)")
                        .HasAnnotation("MySQL:Collation", "utf8_general_ci");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("TenantId_Id");

                    b.ToTable("Role");

                    b.HasAnnotation("MySQL:Charset", "utf8");

                    b.HasAnnotation("MySQL:Collation", "utf8_general_ci");
                });

            modelBuilder.Entity("SaaSEqt.IdentityAccess.Domain.Entities.Tenant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("MySQL:Collation", "utf8_general_ci");

                    b.Property<bool>("Active");

                    b.Property<string>("Description")
                        .HasColumnType("varchar(2000)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("TenantId_Id")
                        .IsRequired()
                        .HasColumnName("TenantId_Id")
                        .HasColumnType("varchar(36)")
                        .HasAnnotation("MySQL:Collation", "utf8_general_ci");

                    b.HasKey("Id");

                    b.HasAlternateKey("TenantId_Id")
                        .HasName("TenantId_Id");

                    b.ToTable("Tenant");

                    b.HasAnnotation("MySQL:Charset", "utf8");

                    b.HasAnnotation("MySQL:Collation", "utf8_general_ci");
                });

            modelBuilder.Entity("SaaSEqt.IdentityAccess.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("MySQL:Collation", "utf8_general_ci");

                    b.Property<string>("Password")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("TenantId_Id")
                        .IsRequired()
                        .HasColumnName("TenantId_Id")
                        .HasColumnType("varchar(36)")
                        .HasAnnotation("MySQL:Collation", "utf8_general_ci");

                    b.Property<string>("Username")
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("TenantId_Id");

                    b.ToTable("User");

                    b.HasAnnotation("MySQL:Charset", "utf8");

                    b.HasAnnotation("MySQL:Collation", "utf8_general_ci");
                });

            modelBuilder.Entity("SaaSEqt.IdentityAccess.Domain.Entities.Group", b =>
                {
                    b.HasOne("SaaSEqt.IdentityAccess.Domain.Entities.Tenant", "Tenant")
                        .WithMany()
                        .HasForeignKey("TenantId_Id")
                        .HasPrincipalKey("TenantId_Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SaaSEqt.IdentityAccess.Domain.Entities.GroupMember", b =>
                {
                    b.HasOne("SaaSEqt.IdentityAccess.Domain.Entities.Group")
                        .WithMany("GroupMembers")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SaaSEqt.IdentityAccess.Domain.Entities.Tenant", "Tenant")
                        .WithMany()
                        .HasForeignKey("TenantId_Id")
                        .HasPrincipalKey("TenantId_Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SaaSEqt.IdentityAccess.Domain.Entities.Person", b =>
                {
                    b.HasOne("SaaSEqt.IdentityAccess.Domain.Entities.Tenant", "Tenant")
                        .WithMany()
                        .HasForeignKey("TenantId_Id")
                        .HasPrincipalKey("TenantId_Id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SaaSEqt.IdentityAccess.Domain.Entities.User", "User")
                        .WithOne("Person")
                        .HasForeignKey("SaaSEqt.IdentityAccess.Domain.Entities.Person", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.OwnsOne("SaaSEqt.IdentityAccess.Domain.Entities.ContactInformation", "ContactInformation", b1 =>
                        {
                            b1.Property<Guid>("PersonId");

                            b1.ToTable("Person","book2business");

                            b1.HasOne("SaaSEqt.IdentityAccess.Domain.Entities.Person")
                                .WithOne("ContactInformation")
                                .HasForeignKey("SaaSEqt.IdentityAccess.Domain.Entities.ContactInformation", "PersonId")
                                .OnDelete(DeleteBehavior.Cascade);

                            b1.OwnsOne("SaaSEqt.IdentityAccess.Domain.Entities.EmailAddress", "EmailAddress", b2 =>
                                {
                                    b2.Property<Guid>("ContactInformationPersonId");

                                    b2.Property<string>("Address")
                                        .HasColumnType("varchar(255)");

                                    b2.ToTable("Person","book2business");

                                    b2.HasOne("SaaSEqt.IdentityAccess.Domain.Entities.ContactInformation")
                                        .WithOne("EmailAddress")
                                        .HasForeignKey("SaaSEqt.IdentityAccess.Domain.Entities.EmailAddress", "ContactInformationPersonId")
                                        .OnDelete(DeleteBehavior.Cascade);
                                });

                            b1.OwnsOne("SaaSEqt.IdentityAccess.Domain.Entities.PostalAddress", "PostalAddress", b2 =>
                                {
                                    b2.Property<Guid>("ContactInformationPersonId");

                                    b2.Property<string>("City")
                                        .HasColumnType("varchar(255)");

                                    b2.Property<string>("CountryCode")
                                        .HasColumnType("varchar(255)");

                                    b2.Property<string>("PostalCode")
                                        .HasColumnType("varchar(255)");

                                    b2.Property<string>("StateProvince")
                                        .HasColumnType("varchar(255)");

                                    b2.Property<string>("StreetAddress")
                                        .HasColumnType("varchar(255)");

                                    b2.ToTable("Person","book2business");

                                    b2.HasOne("SaaSEqt.IdentityAccess.Domain.Entities.ContactInformation")
                                        .WithOne("PostalAddress")
                                        .HasForeignKey("SaaSEqt.IdentityAccess.Domain.Entities.PostalAddress", "ContactInformationPersonId")
                                        .OnDelete(DeleteBehavior.Cascade);
                                });

                            b1.OwnsOne("SaaSEqt.IdentityAccess.Domain.Entities.Telephone", "PrimaryTelephone", b2 =>
                                {
                                    b2.Property<Guid>("ContactInformationPersonId");

                                    b2.Property<string>("Number")
                                        .HasColumnType("varchar(255)");

                                    b2.ToTable("Person","book2business");

                                    b2.HasOne("SaaSEqt.IdentityAccess.Domain.Entities.ContactInformation")
                                        .WithOne("PrimaryTelephone")
                                        .HasForeignKey("SaaSEqt.IdentityAccess.Domain.Entities.Telephone", "ContactInformationPersonId")
                                        .OnDelete(DeleteBehavior.Cascade);
                                });

                            b1.OwnsOne("SaaSEqt.IdentityAccess.Domain.Entities.Telephone", "SecondaryTelephone", b2 =>
                                {
                                    b2.Property<Guid>("ContactInformationPersonId");

                                    b2.Property<string>("Number")
                                        .HasColumnType("varchar(255)");

                                    b2.ToTable("Person","book2business");

                                    b2.HasOne("SaaSEqt.IdentityAccess.Domain.Entities.ContactInformation")
                                        .WithOne("SecondaryTelephone")
                                        .HasForeignKey("SaaSEqt.IdentityAccess.Domain.Entities.Telephone", "ContactInformationPersonId")
                                        .OnDelete(DeleteBehavior.Cascade);
                                });
                        });

                    b.OwnsOne("SaaSEqt.IdentityAccess.Domain.Entities.FullName", "Name", b1 =>
                        {
                            b1.Property<Guid>("PersonId");

                            b1.Property<string>("FirstName")
                                .HasColumnType("varchar(255)");

                            b1.Property<string>("LastName")
                                .HasColumnType("varchar(255)");

                            b1.ToTable("Person","book2business");

                            b1.HasOne("SaaSEqt.IdentityAccess.Domain.Entities.Person")
                                .WithOne("Name")
                                .HasForeignKey("SaaSEqt.IdentityAccess.Domain.Entities.FullName", "PersonId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("SaaSEqt.IdentityAccess.Domain.Entities.Role", b =>
                {
                    b.HasOne("SaaSEqt.IdentityAccess.Domain.Entities.Group", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SaaSEqt.IdentityAccess.Domain.Entities.Tenant", "Tenant")
                        .WithMany()
                        .HasForeignKey("TenantId_Id")
                        .HasPrincipalKey("TenantId_Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SaaSEqt.IdentityAccess.Domain.Entities.User", b =>
                {
                    b.HasOne("SaaSEqt.IdentityAccess.Domain.Entities.Tenant", "Tenant")
                        .WithMany()
                        .HasForeignKey("TenantId_Id")
                        .HasPrincipalKey("TenantId_Id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.OwnsOne("SaaSEqt.IdentityAccess.Domain.Entities.Enablement", "Enablement", b1 =>
                        {
                            b1.Property<Guid>("UserId");

                            b1.Property<bool>("Enabled");

                            b1.Property<DateTime>("EndDate");

                            b1.Property<DateTime>("StartDate");

                            b1.ToTable("User","book2business");

                            b1.HasOne("SaaSEqt.IdentityAccess.Domain.Entities.User")
                                .WithOne("Enablement")
                                .HasForeignKey("SaaSEqt.IdentityAccess.Domain.Entities.Enablement", "UserId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
