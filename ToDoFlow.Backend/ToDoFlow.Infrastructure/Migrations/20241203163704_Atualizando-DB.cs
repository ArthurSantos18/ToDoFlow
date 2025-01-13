using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AtualizandoDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_task_items_categories_category_id",
                table: "task_items");

            migrationBuilder.AddColumn<int>(
                name: "user_id",
                table: "categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_categories_user_id",
                table: "categories",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_categories_users_user_id",
                table: "categories",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_task_items_categories_category_id",
                table: "task_items",
                column: "category_id",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_categories_users_user_id",
                table: "categories");

            migrationBuilder.DropForeignKey(
                name: "FK_task_items_categories_category_id",
                table: "task_items");

            migrationBuilder.DropIndex(
                name: "IX_categories_user_id",
                table: "categories");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "categories");

            migrationBuilder.AddForeignKey(
                name: "FK_task_items_categories_category_id",
                table: "task_items",
                column: "category_id",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
