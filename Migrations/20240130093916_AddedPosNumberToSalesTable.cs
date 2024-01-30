using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PartyFunApi.Migrations
{
    /// <inheritdoc />
    public partial class AddedPosNumberToSalesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Sales",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PosNumber",
                table: "Sales",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Note",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "PosNumber",
                table: "Sales");
        }
    }
}
