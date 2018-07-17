using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Business.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "SaaSEqt.eShop.Services.BusinessDb");

            migrationBuilder.CreateTable(
                name: "Site",
                schema: "book2.business",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(type: "varchar(2000)", nullable: true),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false),
                    TenantId = table.Column<Guid>(type: "char(36)", nullable: false),
                    ContactInformation_ContactName = table.Column<string>(type: "varchar(255)", nullable: true),
                    ContactInformation_EmailAddress = table.Column<string>(type: "varchar(255)", nullable: true),
                    ContactInformation_PrimaryTelephone = table.Column<string>(type: "varchar(255)", nullable: true),
                    ContactInformation_SecondaryTelephone = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Site", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Branding",
                schema: "book2.business",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Logo = table.Column<string>(type: "varchar(4000)", nullable: true),
                    PageColor1 = table.Column<string>(type: "varchar(10)", nullable: true),
                    PageColor2 = table.Column<string>(type: "varchar(10)", nullable: true),
                    PageColor3 = table.Column<string>(type: "varchar(10)", nullable: true),
                    PageColor4 = table.Column<string>(type: "varchar(10)", nullable: true),
                    SiteId = table.Column<Guid>(type: "char(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branding", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Branding_Site_SiteId",
                        column: x => x.SiteId,
                        principalSchema: "book2.business",
                        principalTable: "Site",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                schema: "book2.business",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(type: "varchar(2000)", nullable: true),
                    Image = table.Column<string>(nullable: true),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false),
                    SiteId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Address_City = table.Column<string>(type: "varchar(255)", nullable: true),
                    Address_Country = table.Column<string>(type: "varchar(255)", nullable: true),
                    Address_StateProvince = table.Column<string>(type: "varchar(255)", nullable: true),
                    Address_Street = table.Column<string>(type: "varchar(255)", nullable: true),
                    Address_ZipCode = table.Column<string>(type: "varchar(255)", nullable: true),
                    ContactInformation_ContactName = table.Column<string>(type: "varchar(255)", nullable: true),
                    ContactInformation_EmailAddress = table.Column<string>(type: "varchar(255)", nullable: true),
                    ContactInformation_PrimaryTelephone = table.Column<string>(type: "varchar(255)", nullable: true),
                    ContactInformation_SecondaryTelephone = table.Column<string>(type: "varchar(255)", nullable: true),
                    Geolocation_Latitude = table.Column<double>(nullable: false),
                    Geolocation_Longitude = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Location_Site_SiteId",
                        column: x => x.SiteId,
                        principalSchema: "book2.business",
                        principalTable: "Site",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Staff",
                schema: "book2.business",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Bio = table.Column<string>(type: "varchar(2000)", nullable: true),
                    CanLoginAllLocations = table.Column<bool>(nullable: false),
                    Image = table.Column<string>(type: "varchar(4000)", nullable: true),
                    IsMale = table.Column<bool>(nullable: false),
                    SiteId = table.Column<Guid>(type: "char(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Staff_Site_SiteId",
                        column: x => x.SiteId,
                        principalSchema: "book2.business",
                        principalTable: "Site",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LocationImage",
                schema: "book2.business",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Image = table.Column<string>(nullable: true),
                    LocationId = table.Column<Guid>(type: "char(36)", nullable: false),
                    SiteId = table.Column<Guid>(type: "char(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocationImage_Location_LocationId",
                        column: x => x.LocationId,
                        principalSchema: "book2.business",
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LocationImage_Site_SiteId",
                        column: x => x.SiteId,
                        principalSchema: "book2.business",
                        principalTable: "Site",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StaffLoginLocation",
                schema: "book2.business",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    StaffId = table.Column<Guid>(nullable: false),
                    LocationId = table.Column<Guid>(type: "char(36)", nullable: false),
                    SiteId = table.Column<Guid>(type: "char(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffLoginLocation", x => new { x.Id, x.StaffId, x.LocationId });
                    table.ForeignKey(
                        name: "FK_StaffLoginLocation_Location_LocationId",
                        column: x => x.LocationId,
                        principalSchema: "book2.business",
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StaffLoginLocation_Site_SiteId",
                        column: x => x.SiteId,
                        principalSchema: "book2.business",
                        principalTable: "Site",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StaffLoginLocation_Staff_StaffId",
                        column: x => x.StaffId,
                        principalSchema: "book2.business",
                        principalTable: "Staff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Branding_SiteId",
                schema: "book2.business",
                table: "Branding",
                column: "SiteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Location_SiteId",
                schema: "book2.business",
                table: "Location",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationImage_LocationId",
                schema: "book2.business",
                table: "LocationImage",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationImage_SiteId",
                schema: "book2.business",
                table: "LocationImage",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_SiteId",
                schema: "book2.business",
                table: "Staff",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffLoginLocation_LocationId",
                schema: "book2.business",
                table: "StaffLoginLocation",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffLoginLocation_SiteId",
                schema: "book2.business",
                table: "StaffLoginLocation",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffLoginLocation_StaffId",
                schema: "book2.business",
                table: "StaffLoginLocation",
                column: "StaffId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Branding",
                schema: "book2.business");

            migrationBuilder.DropTable(
                name: "LocationImage",
                schema: "book2.business");

            migrationBuilder.DropTable(
                name: "StaffLoginLocation",
                schema: "book2.business");

            migrationBuilder.DropTable(
                name: "Location",
                schema: "book2.business");

            migrationBuilder.DropTable(
                name: "Staff",
                schema: "book2.business");

            migrationBuilder.DropTable(
                name: "Site",
                schema: "book2.business");
        }
    }
}
