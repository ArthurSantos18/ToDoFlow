using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AtualizarDB3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_categories_users_user_id",
                table: "categories");

            migrationBuilder.AddForeignKey(
                name: "FK_categories_users_user_id",
                table: "categories",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_categories_users_user_id",
                table: "categories");

            migrationBuilder.AddForeignKey(
                name: "FK_categories_users_user_id",
                table: "categories",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
