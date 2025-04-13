using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dev.API.Migrations
{
    /// <inheritdoc />
    public partial class RemovedcolumnDifficultyu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Review",
                table: "Difficulties");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Review",
                table: "Difficulties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
