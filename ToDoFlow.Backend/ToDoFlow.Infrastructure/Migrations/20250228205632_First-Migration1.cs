using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ToDoFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(100)", nullable: false),
                    email = table.Column<string>(type: "varchar(100)", nullable: false),
                    password_hash = table.Column<string>(type: "varchar(60)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    profile = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(30)", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.id);
                    table.ForeignKey(
                        name: "FK_categories_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_refresh_token",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    refresh_token = table.Column<string>(type: "varchar(100)", nullable: false),
                    expiration = table.Column<DateTime>(type: "datetime", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_refresh_token", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_refresh_token_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "task_items",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(60)", nullable: false),
                    description = table.Column<string>(type: "varchar(300)", nullable: false),
                    status = table.Column<byte>(type: "tinyint", nullable: false),
                    priority = table.Column<byte>(type: "tinyint", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    complete_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    category_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_task_items", x => x.id);
                    table.ForeignKey(
                        name: "FK_task_items_categories_category_id",
                        column: x => x.category_id,
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "created_at", "email", "name", "password_hash", "profile" },
                values: new object[,]
                {
                    { 1, new DateTime(1986, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", "Admin", "$2a$11$ZVHygbDAmzxjzbIEOLPBluUfUToFaqskwUO4r7YzWQSlJJ9DWwKhq", (byte)0 },
                    { 2, new DateTime(1986, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "doloris@gmail.com", "Misumi Uika", "$2a$11$nmsDFSlnFp4QFOZ76qfBOeF4H7AxA2Tc6zASVRw/2..MqPELfZT6C", (byte)1 }
                });

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "name", "user_id" },
                values: new object[,]
                {
                    { 1, "Home", 1 },
                    { 2, "Shopping", 1 },
                    { 3, "Work", 2 }
                });

            migrationBuilder.InsertData(
                table: "user_refresh_token",
                columns: new[] { "id", "expiration", "refresh_token", "user_id" },
                values: new object[,]
                {
                    { 1, new DateTime(1986, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 1 },
                    { 2, new DateTime(1986, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 2 }
                });

            migrationBuilder.InsertData(
                table: "task_items",
                columns: new[] { "id", "category_id", "complete_at", "created_at", "description", "name", "priority", "status" },
                values: new object[,]
                {
                    { 1, 1, null, new DateTime(1986, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Clean all the dirty dishes, including plates, glasses, and utensils.", "Wash the dishes", (byte)1, (byte)1 },
                    { 2, 1, null, new DateTime(1986, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Vacuum the floors, mop the surfaces, and tidy up the rooms.", "Clean the house", (byte)2, (byte)1 },
                    { 3, 2, null, new DateTime(1986, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Buy groceries including vegetables, fruits, bread, and milk.", "Grocery shopping", (byte)3, (byte)1 },
                    { 4, 2, null, new DateTime(1986, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Buy new headphones and a phone charger.", "Electronics shopping", (byte)0, (byte)1 },
                    { 5, 3, null, new DateTime(1986, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Finalize the report for the current project, including graphs and conclusions.", "Complete project report", (byte)2, (byte)1 },
                    { 6, 3, null, new DateTime(1986, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Join the weekly team meeting to discuss project progress and goals.", "Attend team meeting", (byte)1, (byte)1 },
                    { 7, 3, null, new DateTime(1986, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Go through and reply to important work-related emails.", "Check emails", (byte)0, (byte)1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_categories_user_id",
                table: "categories",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_task_items_category_id",
                table: "task_items",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_refresh_token_user_id",
                table: "user_refresh_token",
                column: "user_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "task_items");

            migrationBuilder.DropTable(
                name: "user_refresh_token");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
