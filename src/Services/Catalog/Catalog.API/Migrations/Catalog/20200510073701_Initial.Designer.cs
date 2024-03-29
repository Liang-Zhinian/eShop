﻿// <auto-generated />
using Eva.eShop.Services.Catalog.API.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Catalog.API.Migrations.Catalog
{
    [DbContext(typeof(CatalogContext))]
    [Migration("20200510073701_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Eva.eShop.Services.Catalog.API.Model.CatalogBrand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("CatalogBrand");
                });

            modelBuilder.Entity("Eva.eShop.Services.Catalog.API.Model.CatalogItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AvailableStock");

                    b.Property<int>("CatalogBrandId");

                    b.Property<int>("CatalogTypeId");

                    b.Property<string>("Description");

                    b.Property<int>("MaxStockThreshold");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<bool>("OnReorder");

                    b.Property<string>("PictureFileName");

                    b.Property<decimal>("Price");

                    b.Property<int>("RestockThreshold");

                    b.HasKey("Id");

                    b.HasIndex("CatalogBrandId");

                    b.HasIndex("CatalogTypeId");

                    b.ToTable("Catalog");
                });

            modelBuilder.Entity("Eva.eShop.Services.Catalog.API.Model.CatalogType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("CatalogType");
                });

            modelBuilder.Entity("Eva.eShop.Services.Catalog.API.Model.CatalogItem", b =>
                {
                    b.HasOne("Eva.eShop.Services.Catalog.API.Model.CatalogBrand", "CatalogBrand")
                        .WithMany()
                        .HasForeignKey("CatalogBrandId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Eva.eShop.Services.Catalog.API.Model.CatalogType", "CatalogType")
                        .WithMany()
                        .HasForeignKey("CatalogTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
