using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YouScout.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Follows : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_app_users_AspNetUsers_identity_user_id",
                table: "app_users");

            migrationBuilder.EnsureSchema(
                name: "dev");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                newName: "AspNetUserTokens",
                newSchema: "dev");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                newName: "AspNetUsers",
                newSchema: "dev");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                newName: "AspNetUserRoles",
                newSchema: "dev");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                newName: "AspNetUserLogins",
                newSchema: "dev");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                newName: "AspNetUserClaims",
                newSchema: "dev");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                newName: "AspNetRoles",
                newSchema: "dev");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                newName: "AspNetRoleClaims",
                newSchema: "dev");

            migrationBuilder.RenameTable(
                name: "app_users",
                newName: "user_profiles",
                newSchema: "dev");

            migrationBuilder.CreateTable(
                name: "follows",
                schema: "dev",
                columns: table => new
                {
                    follower_id = table.Column<Guid>(type: "uuid", nullable: false),
                    following_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    follow_type = table.Column<string>(type: "text", nullable: false, defaultValue: "User"),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_follows", x => new { x.follower_id, x.following_user_id });
                    table.ForeignKey(
                        name: "FK_follows_user_profiles_follower_id",
                        column: x => x.follower_id,
                        principalSchema: "dev",
                        principalTable: "user_profiles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_follows_user_profiles_following_user_id",
                        column: x => x.following_user_id,
                        principalSchema: "dev",
                        principalTable: "user_profiles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_follows_following_user_id",
                schema: "dev",
                table: "follows",
                column: "following_user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_user_profiles_AspNetUsers_identity_user_id",
                schema: "dev",
                table: "user_profiles",
                column: "identity_user_id",
                principalSchema: "dev",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_profiles_AspNetUsers_identity_user_id",
                schema: "dev",
                table: "user_profiles");

            migrationBuilder.DropTable(
                name: "follows",
                schema: "dev");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                schema: "dev",
                newName: "AspNetUserTokens");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                schema: "dev",
                newName: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                schema: "dev",
                newName: "AspNetUserRoles");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                schema: "dev",
                newName: "AspNetUserLogins");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                schema: "dev",
                newName: "AspNetUserClaims");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                schema: "dev",
                newName: "AspNetRoles");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                schema: "dev",
                newName: "AspNetRoleClaims");

            migrationBuilder.RenameTable(
                name: "user_profiles",
                schema: "dev",
                newName: "app_users");

            migrationBuilder.AddForeignKey(
                name: "FK_app_users_AspNetUsers_identity_user_id",
                table: "app_users",
                column: "identity_user_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
