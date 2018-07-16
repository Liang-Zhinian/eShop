using Microsoft.EntityFrameworkCore.Metadata;
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
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Brand = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogBrand", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CatalogType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
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
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AvailableStock = table.Column<int>(nullable: false),
                    CatalogBrandId = table.Column<int>(nullable: false),
                    CatalogTypeId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    MaxStockThreshold = table.Column<int>(nullable: false),
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
                    IndustryStandardCategoryId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false),
                    Price = table.Column<double>(nullable: false),
                    SchedulableCatalogTypeId = table.Column<Guid>(nullable: false),
                    ServiceCategoryId = table.Column<Guid>(type: "char(36)", nullable: false),
                    SiteId = table.Column<Guid>(nullable: false),
                    TaxRate = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceItem_ServiceCategory_SchedulableCatalogTypeId",
                        column: x => x.SchedulableCatalogTypeId,
                        principalTable: "ServiceCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Availability",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    BookableEndDateTime = table.Column<DateTime>(nullable: false),
                    EndDateTime = table.Column<DateTime>(nullable: false),
                    Friday = table.Column<bool>(nullable: false),
                    LocationId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Monday = table.Column<bool>(nullable: false),
                    Saturday = table.Column<bool>(nullable: false),
                    SchedulableCatalogItemId = table.Column<Guid>(type: "char(36)", nullable: false),
                    StaffId = table.Column<Guid>(type: "char(36)", nullable: false),
                    StartDateTime = table.Column<DateTime>(nullable: false),
                    Sunday = table.Column<bool>(nullable: false),
                    Thursday = table.Column<bool>(nullable: false),
                    Tuesday = table.Column<bool>(nullable: false),
                    Wednesday = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Availability", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Availability_ServiceItem_SchedulableCatalogItemId",
                        column: x => x.SchedulableCatalogItemId,
                        principalTable: "ServiceItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Unavailability",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Description = table.Column<string>(type: "varchar(2000)", nullable: true),
                    EndDateTime = table.Column<DateTime>(nullable: false),
                    Friday = table.Column<bool>(nullable: false),
                    LocationId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Monday = table.Column<bool>(nullable: false),
                    Saturday = table.Column<bool>(nullable: false),
                    SchedulableCatalogItemId = table.Column<Guid>(type: "char(36)", nullable: false),
                    StaffId = table.Column<Guid>(type: "char(36)", nullable: false),
                    StartDateTime = table.Column<DateTime>(nullable: false),
                    Sunday = table.Column<bool>(nullable: false),
                    Thursday = table.Column<bool>(nullable: false),
                    Tuesday = table.Column<bool>(nullable: false),
                    Wednesday = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unavailability", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Unavailability_ServiceItem_SchedulableCatalogItemId",
                        column: x => x.SchedulableCatalogItemId,
                        principalTable: "ServiceItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Availability_SchedulableCatalogItemId",
                table: "Availability",
                column: "SchedulableCatalogItemId");

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
                name: "IX_ServiceItem_SchedulableCatalogTypeId",
                table: "ServiceItem",
                column: "SchedulableCatalogTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Unavailability_SchedulableCatalogItemId",
                table: "Unavailability",
                column: "SchedulableCatalogItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Availability");

            migrationBuilder.DropTable(
                name: "Catalog");

            migrationBuilder.DropTable(
                name: "Unavailability");

            migrationBuilder.DropTable(
                name: "CatalogBrand");

            migrationBuilder.DropTable(
                name: "CatalogType");

            migrationBuilder.DropTable(
                name: "ServiceItem");

            migrationBuilder.DropTable(
                name: "ServiceCategory");

            migrationBuilder.DropTable(
                name: "ScheduleType");
        }
    }
}
