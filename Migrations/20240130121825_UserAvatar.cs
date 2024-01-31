using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PartyFunApi.Migrations
{
    /// <inheritdoc />
    public partial class UserAvatar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AvatarPublicId",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PublidId",
                table: "ProductImages",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AvatarPublicId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PublidId",
                table: "ProductImages");
        }
    }
}
