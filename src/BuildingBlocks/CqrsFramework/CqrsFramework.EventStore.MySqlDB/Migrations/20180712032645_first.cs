using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CqrsFramework.EventStore.MySqlDB.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AggregateId = table.Column<Guid>(nullable: false),
                    AggregateType = table.Column<string>(nullable: true),
                    CorrelationId = table.Column<string>(nullable: true),
                    EventType = table.Column<string>(nullable: false),
                    Payload = table.Column<string>(nullable: false),
                    State = table.Column<int>(nullable: false),
                    TimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    Version = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events");
        }
    }
}
