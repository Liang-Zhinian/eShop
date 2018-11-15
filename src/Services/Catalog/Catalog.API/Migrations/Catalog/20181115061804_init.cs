using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Catalog.API.Migrations.Catalog
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CatalogBrand",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Brand = table.Column<string>(maxLength: 100, nullable: false),
                    MerchantId = table.Column<Guid>(type: "char(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogBrand", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CatalogType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    MerchantId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Type = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValue: 1),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Catalog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    AvailableStock = table.Column<int>(nullable: false),
                    CatalogBrandId = table.Column<Guid>(nullable: false),
                    CatalogTypeId = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    MaxStockThreshold = table.Column<int>(nullable: false),
                    MerchantId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    OnReorder = table.Column<bool>(nullable: false),
                    PictureFileName = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    RestockThreshold = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catalog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Catalog_CatalogBrand_CatalogBrandId",
                        column: x => x.CatalogBrandId,
                        principalTable: "CatalogBrand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Catalog_CatalogType_CatalogTypeId",
                        column: x => x.CatalogTypeId,
                        principalTable: "CatalogType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceCategory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    AllowOnlineScheduling = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(type: "varchar(2000)", nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false),
                    ScheduleTypeId = table.Column<int>(nullable: false),
                    SiteId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceCategory_ScheduleType_ScheduleTypeId",
                        column: x => x.ScheduleTypeId,
                        principalTable: "ScheduleType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    AllowOnlineScheduling = table.Column<bool>(nullable: false),
                    DefaultTimeLength = table.Column<int>(nullable: false),
                    Description = table.Column<string>(type: "varchar(2000)", nullable: false),
                    IndustryStandardCategoryName = table.Column<string>(type: "varchar(255)", nullable: false),
                    IndustryStandardSubcategoryName = table.Column<string>(type: "varchar(255)", nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false),
                    Price = table.Column<double>(nullable: false),
                    ServiceCategoryId = table.Column<Guid>(type: "char(36)", nullable: false),
                    SiteId = table.Column<Guid>(type: "char(36)", nullable: false),
                    TaxRate = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceItem_ServiceCategory_ServiceCategoryId",
                        column: x => x.ServiceCategoryId,
                        principalTable: "ServiceCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Catalog_CatalogBrandId",
                table: "Catalog",
                column: "CatalogBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Catalog_CatalogTypeId",
                table: "Catalog",
                column: "CatalogTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCategory_ScheduleTypeId",
                table: "ServiceCategory",
                column: "ScheduleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceItem_ServiceCategoryId",
                table: "ServiceItem",
                column: "ServiceCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Catalog");

            migrationBuilder.DropTable(
                name: "ServiceItem");

            migrationBuilder.DropTable(
                name: "CatalogBrand");

            migrationBuilder.DropTable(
                name: "CatalogType");

            migrationBuilder.DropTable(
                name: "ServiceCategory");

            migrationBuilder.DropTable(
                name: "ScheduleType");
        }
    }
}
