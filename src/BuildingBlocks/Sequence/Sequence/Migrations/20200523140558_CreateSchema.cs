using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sequence.Migrations
{
    public partial class CreateSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbKeyAlloc",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SequenceName = table.Column<string>(nullable: false),
                    IncrementSize = table.Column<long>(nullable: false),
                    MinValue = table.Column<long>(nullable: false),
                    MaxValue = table.Column<long>(nullable: false),
                    NextValue = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbKeyAlloc", x => x.Id);
                    table.UniqueConstraint("AK_tbKeyAlloc_SequenceName", x => x.SequenceName);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbKeyAlloc");
        }
    }
}
