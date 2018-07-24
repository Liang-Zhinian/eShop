using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SaaSEqt.IdentityAccess.Infra.Data.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "book2business");

            migrationBuilder.CreateTable(
                name: "Tenant",
                schema: "book2business",
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
                schema: "book2business",
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
                        principalSchema: "book2business",
                        principalTable: "Tenant",
                        principalColumn: "TenantId_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "book2business",
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
                        principalSchema: "book2business",
                        principalTable: "Tenant",
                        principalColumn: "TenantId_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupMember",
                schema: "book2business",
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
                        principalSchema: "book2business",
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupMember_Tenant_TenantId_Id",
                        column: x => x.TenantId_Id,
                        principalSchema: "book2business",
                        principalTable: "Tenant",
                        principalColumn: "TenantId_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                schema: "book2business",
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
                        principalSchema: "book2business",
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Role_Tenant_TenantId_Id",
                        column: x => x.TenantId_Id,
                        principalSchema: "book2business",
                        principalTable: "Tenant",
                        principalColumn: "TenantId_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                schema: "book2business",
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
                        principalSchema: "book2business",
                        principalTable: "Tenant",
                        principalColumn: "TenantId_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Person_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "book2business",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Group_TenantId_Id",
                schema: "book2business",
                table: "Group",
                column: "TenantId_Id");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMember_GroupId",
                schema: "book2business",
                table: "GroupMember",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMember_TenantId_Id",
                schema: "book2business",
                table: "GroupMember",
                column: "TenantId_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Person_TenantId_Id",
                schema: "book2business",
                table: "Person",
                column: "TenantId_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Person_UserId",
                schema: "book2business",
                table: "Person",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Role_GroupId",
                schema: "book2business",
                table: "Role",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_TenantId_Id",
                schema: "book2business",
                table: "Role",
                column: "TenantId_Id");

            migrationBuilder.CreateIndex(
                name: "IX_User_TenantId_Id",
                schema: "book2business",
                table: "User",
                column: "TenantId_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupMember",
                schema: "book2business");

            migrationBuilder.DropTable(
                name: "Person",
                schema: "book2business");

            migrationBuilder.DropTable(
                name: "Role",
                schema: "book2business");

            migrationBuilder.DropTable(
                name: "User",
                schema: "book2business");

            migrationBuilder.DropTable(
                name: "Group",
                schema: "book2business");

            migrationBuilder.DropTable(
                name: "Tenant",
                schema: "book2business");
        }
    }
}
