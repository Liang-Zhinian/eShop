using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SaaSEqt.IdentityAccess.Infrastructure.Migrations
{
    public partial class addRegistrationInvitation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailAddress_Address",
                table: "Person");

            migrationBuilder.CreateTable(
                name: "RegistrationInvitation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Description = table.Column<string>(type: "varchar(2000)", nullable: true),
                    InvitationId = table.Column<string>(type: "varchar(36)", nullable: false),
                    StartingOn = table.Column<DateTime>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    Until = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrationInvitation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegistrationInvitation_Tenant_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationInvitation_TenantId",
                table: "RegistrationInvitation",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegistrationInvitation");

            migrationBuilder.AddColumn<string>(
                name: "EmailAddress_Address",
                table: "Person",
                type: "varchar(255)",
                nullable: true);
        }
    }
}
