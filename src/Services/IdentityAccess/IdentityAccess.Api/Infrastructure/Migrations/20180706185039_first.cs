using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SaaSEqt.IdentityAccess.API.Infrastructure.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tenant",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(type: "varchar(2000)", nullable: true),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false),
                    TenantId_Id = table.Column<string>(type: "varchar(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenant", x => x.Id);
                    table.UniqueConstraint("TenantId_Id", x => x.TenantId_Id);
                });

            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(type: "varchar(2000)", nullable: true),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false),
                    TenantId_Id = table.Column<string>(type: "varchar(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Group_Tenant_TenantId_Id",
                        column: x => x.TenantId_Id,
                        principalTable: "Tenant",
                        principalColumn: "TenantId_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Password = table.Column<string>(type: "varchar(255)", nullable: true),
                    TenantId_Id = table.Column<string>(type: "varchar(36)", nullable: false),
                    Username = table.Column<string>(type: "varchar(255)", nullable: true),
                    Enablement_Enabled = table.Column<bool>(nullable: false),
                    Enablement_EndDate = table.Column<DateTime>(nullable: false),
                    Enablement_StartDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Tenant_TenantId_Id",
                        column: x => x.TenantId_Id,
                        principalTable: "Tenant",
                        principalColumn: "TenantId_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupMember",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    GroupId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false),
                    TenantId_Id = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupMember", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupMember_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupMember_Tenant_TenantId_Id",
                        column: x => x.TenantId_Id,
                        principalTable: "Tenant",
                        principalColumn: "TenantId_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(type: "varchar(2000)", nullable: true),
                    GroupId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false),
                    SupportsNesting = table.Column<bool>(nullable: false),
                    TenantId_Id = table.Column<string>(type: "varchar(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Role_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Role_Tenant_TenantId_Id",
                        column: x => x.TenantId_Id,
                        principalTable: "Tenant",
                        principalColumn: "TenantId_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenantId_Id = table.Column<string>(type: "varchar(36)", nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    ContactInformation_EmailAddress_Address = table.Column<string>(type: "varchar(255)", nullable: true),
                    Name_FirstName = table.Column<string>(type: "varchar(255)", nullable: true),
                    Name_LastName = table.Column<string>(type: "varchar(255)", nullable: true),
                    ContactInformation_PostalAddress_City = table.Column<string>(type: "varchar(255)", nullable: true),
                    ContactInformation_PostalAddress_CountryCode = table.Column<string>(type: "varchar(255)", nullable: true),
                    ContactInformation_PostalAddress_PostalCode = table.Column<string>(type: "varchar(255)", nullable: true),
                    ContactInformation_PostalAddress_StateProvince = table.Column<string>(type: "varchar(255)", nullable: true),
                    ContactInformation_PostalAddress_StreetAddress = table.Column<string>(type: "varchar(255)", nullable: true),
                    ContactInformation_PrimaryTelephone_Number = table.Column<string>(type: "varchar(255)", nullable: true),
                    ContactInformation_SecondaryTelephone_Number = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Person_Tenant_TenantId_Id",
                        column: x => x.TenantId_Id,
                        principalTable: "Tenant",
                        principalColumn: "TenantId_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Person_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Group_TenantId_Id",
                table: "Group",
                column: "TenantId_Id");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMember_GroupId",
                table: "GroupMember",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMember_TenantId_Id",
                table: "GroupMember",
                column: "TenantId_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Person_TenantId_Id",
                table: "Person",
                column: "TenantId_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Person_UserId",
                table: "Person",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Role_GroupId",
                table: "Role",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_TenantId_Id",
                table: "Role",
                column: "TenantId_Id");

            migrationBuilder.CreateIndex(
                name: "IX_User_TenantId_Id",
                table: "User",
                column: "TenantId_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupMember");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Group");

            migrationBuilder.DropTable(
                name: "Tenant");
        }
    }
}
