using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Sites.API.Migrations.Sites
{
    public partial class addClientTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gender",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValue: 1),
                    Name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gender", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    AvatarImage = table.Column<byte[]>(type: "MediumBlob", nullable: true),
                    Birthdate = table.Column<DateTime>(nullable: true),
                    DateOfFirstAppointment = table.Column<DateTime>(nullable: true),
                    EmailAddress = table.Column<string>(type: "varchar(255)", nullable: true),
                    FirstName = table.Column<string>(type: "varchar(255)", nullable: true),
                    GenderId = table.Column<int>(nullable: false),
                    HomePhone = table.Column<string>(type: "varchar(255)", nullable: true),
                    LastName = table.Column<string>(type: "varchar(255)", nullable: false),
                    MobilePhone = table.Column<string>(type: "varchar(255)", nullable: false),
                    Notes = table.Column<string>(type: "varchar(2000)", nullable: true),
                    WorkPhone = table.Column<string>(type: "varchar(255)", nullable: true),
                    Address_City = table.Column<string>(type: "varchar(255)", nullable: true),
                    Address_Country = table.Column<string>(type: "varchar(255)", nullable: true),
                    Address_StateProvince = table.Column<string>(type: "varchar(255)", nullable: true),
                    Address_Street = table.Column<string>(type: "varchar(255)", nullable: true),
                    Address_ZipCode = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Client_Gender_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Gender",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Client_GenderId",
                table: "Client",
                column: "GenderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "Gender");
        }
    }
}
