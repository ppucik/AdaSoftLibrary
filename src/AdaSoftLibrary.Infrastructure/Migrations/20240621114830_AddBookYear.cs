using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdaSoftLibrary.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBookYear : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Book",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Year",
                table: "Book");
        }
    }
}
