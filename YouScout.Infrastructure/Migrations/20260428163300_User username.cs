using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YouScout.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Userusername : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "username",
                table: "app_users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "ux_app_users_username",
                table: "app_users",
                column: "username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ux_app_users_username",
                table: "app_users");

            migrationBuilder.DropColumn(
                name: "username",
                table: "app_users");
        }
    }
}
