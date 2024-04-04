using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BinanceAcountViewer.Migrations
{
    /// <inheritdoc />
    public partial class Inital1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Pair",
                table: "Sessions",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pair",
                table: "Sessions");
        }
    }
}
