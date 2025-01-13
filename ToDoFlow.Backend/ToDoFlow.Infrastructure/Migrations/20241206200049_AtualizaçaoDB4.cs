using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AtualizaçaoDB4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_task_items_categories_category_id",
                table: "task_items");

            migrationBuilder.AddForeignKey(
                name: "FK_task_items_categories_category_id",
                table: "task_items",
                column: "category_id",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_task_items_categories_category_id",
                table: "task_items");

            migrationBuilder.AddForeignKey(
                name: "FK_task_items_categories_category_id",
                table: "task_items",
                column: "category_id",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
