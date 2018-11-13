using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Identity.API.Migrations.ApplicationDb
{
    public partial class addExternalAccountsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "AvatarImage",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "MediumBlob",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ExternalAccounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AlipayOpenId = table.Column<string>(nullable: true),
                    AlipayUsername = table.Column<string>(nullable: true),
                    FacebookEmail = table.Column<string>(nullable: true),
                    TwitterUsername = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    WechatOpenId = table.Column<string>(nullable: true),
                    WechatUsername = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExternalAccounts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExternalAccounts_UserId",
                table: "ExternalAccounts",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExternalAccounts");

            migrationBuilder.AlterColumn<byte[]>(
                name: "AvatarImage",
                table: "AspNetUsers",
                type: "MediumBlob",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldNullable: true);
        }
    }
}
