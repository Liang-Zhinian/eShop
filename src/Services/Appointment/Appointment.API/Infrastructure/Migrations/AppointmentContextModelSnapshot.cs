﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using SaaSEqt.eShop.Services.Appointment.Infrastructure;
using System;

namespace Appointment.API.Migrations
{
    [DbContext(typeof(AppointmentContext))]
    partial class AppointmentContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026");

            modelBuilder.Entity("SaaSEqt.eShop.Services.Appointment.Domain.AggregatesModel.AppointmentAggregate.Appointment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AppointmentId");

                    b.Property<int>("AppointmentStatusId");

                    b.Property<Guid>("ClientId");

                    b.Property<int>("Duration");

                    b.Property<DateTime>("EndDateTime");

                    b.Property<bool>("FirstAppointment");

                    b.Property<string>("GenderPreference");

                    b.Property<Guid>("LocationId");

                    b.Property<string>("Notes");

                    b.Property<DateTime>("OrderDate");

                    b.Property<Guid?>("PaymentMethodId");

                    b.Property<Guid>("SiteId");

                    b.Property<Guid>("StaffId");

                    b.Property<bool>("StaffRequested");

                    b.Property<DateTime>("StartDateTime");

                    b.HasKey("Id");

                    b.HasIndex("AppointmentStatusId");

                    b.HasIndex("PaymentMethodId");

                    b.ToTable("Appointment");
                });

            modelBuilder.Entity("SaaSEqt.eShop.Services.Appointment.Domain.AggregatesModel.AppointmentAggregate.AppointmentResource", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("AppointmentId");

                    b.Property<Guid>("ResourceId");

                    b.Property<string>("ResourceName");

                    b.Property<Guid>("SiteId");

                    b.HasKey("Id");

                    b.HasIndex("AppointmentId");

                    b.ToTable("AppointmentResources");
                });

            modelBuilder.Entity("SaaSEqt.eShop.Services.Appointment.Domain.AggregatesModel.AppointmentAggregate.AppointmentServiceItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("AllowOnlineScheduling");

                    b.Property<Guid>("AppointmentId")
                        .HasColumnType("char(36)");

                    b.Property<int>("DefaultTimeLength");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(2000)");

                    b.Property<double>("Discount");

                    b.Property<string>("IndustryStandardCategoryName")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("IndustryStandardSubcategoryName")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<double>("Price");

                    b.Property<Guid>("ServiceItemId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("SiteId")
                        .HasColumnType("char(36)");

                    b.Property<double>("TaxRate");

                    b.HasKey("Id");

                    b.HasIndex("AppointmentId");

                    b.ToTable("AppointmentServiceItem");
                });

            modelBuilder.Entity("SaaSEqt.eShop.Services.Appointment.Domain.AggregatesModel.AppointmentAggregate.AppointmentStatus", b =>
                {
                    b.Property<int>("Id")
                        .HasDefaultValue(1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("AppointmentStatus");
                });

            modelBuilder.Entity("SaaSEqt.eShop.Services.Appointment.Domain.AggregatesModel.BuyerAggregate.Buyer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("IdentityGuid")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("IdentityGuid")
                        .IsUnique();

                    b.ToTable("buyers");
                });

            modelBuilder.Entity("SaaSEqt.eShop.Services.Appointment.Domain.AggregatesModel.BuyerAggregate.CardType", b =>
                {
                    b.Property<int>("Id")
                        .HasDefaultValue(1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("cardtypes");
                });

            modelBuilder.Entity("SaaSEqt.eShop.Services.Appointment.Domain.AggregatesModel.BuyerAggregate.PaymentMethod", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Alias")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<Guid>("BuyerId");

                    b.Property<string>("CardHolderName")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("CardNumber")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.Property<int>("CardTypeId");

                    b.Property<DateTime>("Expiration");

                    b.HasKey("Id");

                    b.HasIndex("BuyerId");

                    b.HasIndex("CardTypeId");

                    b.ToTable("paymentmethods");
                });

            modelBuilder.Entity("SaaSEqt.eShop.Services.Appointment.Infrastructure.Idempotency.ClientRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<DateTime>("Time");

                    b.HasKey("Id");

                    b.ToTable("requests");
                });

            modelBuilder.Entity("SaaSEqt.eShop.Services.Appointment.Domain.AggregatesModel.AppointmentAggregate.Appointment", b =>
                {
                    b.HasOne("SaaSEqt.eShop.Services.Appointment.Domain.AggregatesModel.AppointmentAggregate.AppointmentStatus", "AppointmentStatus")
                        .WithMany()
                        .HasForeignKey("AppointmentStatusId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SaaSEqt.eShop.Services.Appointment.Domain.AggregatesModel.BuyerAggregate.PaymentMethod")
                        .WithMany()
                        .HasForeignKey("PaymentMethodId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("SaaSEqt.eShop.Services.Appointment.Domain.AggregatesModel.AppointmentAggregate.AppointmentResource", b =>
                {
                    b.HasOne("SaaSEqt.eShop.Services.Appointment.Domain.AggregatesModel.AppointmentAggregate.Appointment")
                        .WithMany("AppointmentResources")
                        .HasForeignKey("AppointmentId");
                });

            modelBuilder.Entity("SaaSEqt.eShop.Services.Appointment.Domain.AggregatesModel.AppointmentAggregate.AppointmentServiceItem", b =>
                {
                    b.HasOne("SaaSEqt.eShop.Services.Appointment.Domain.AggregatesModel.AppointmentAggregate.Appointment")
                        .WithMany("AppointmentServiceItems")
                        .HasForeignKey("AppointmentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SaaSEqt.eShop.Services.Appointment.Domain.AggregatesModel.BuyerAggregate.PaymentMethod", b =>
                {
                    b.HasOne("SaaSEqt.eShop.Services.Appointment.Domain.AggregatesModel.BuyerAggregate.Buyer")
                        .WithMany("PaymentMethods")
                        .HasForeignKey("BuyerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SaaSEqt.eShop.Services.Appointment.Domain.AggregatesModel.BuyerAggregate.CardType", "CardType")
                        .WithMany()
                        .HasForeignKey("CardTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
