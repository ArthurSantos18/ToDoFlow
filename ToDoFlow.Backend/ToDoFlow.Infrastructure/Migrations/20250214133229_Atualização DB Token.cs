using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AtualizaçãoDBToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user_refresh_token",
                columns: table => new
                {
                    refresh_token = table.Column<string>(type: "varchar(100)", nullable: false),
                    id = table.Column<int>(type: "int", nullable: false),
                    expiration = table.Column<DateTime>(type: "datetime", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_refresh_token", x => x.refresh_token);
                    table.ForeignKey(
                        name: "FK_user_refresh_token_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "user_refresh_token");
        }
    }
}
