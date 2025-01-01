using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initial_create : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TMS_ROLE",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TMS_ROLE", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TMS_TASK_ITEM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Priority = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TMS_TASK_ITEM", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TMS_USER",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TMS_USER", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TMS_USER_TMS_ROLE_RoleId",
                        column: x => x.RoleId,
                        principalTable: "TMS_ROLE",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TMS_USER_TASK",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TaskItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TMS_USER_TASK", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TMS_USER_TASK_TMS_TASK_ITEM_TaskItemId",
                        column: x => x.TaskItemId,
                        principalTable: "TMS_TASK_ITEM",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TMS_USER_TASK_TMS_USER_UserId",
                        column: x => x.UserId,
                        principalTable: "TMS_USER",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TMS_USER_RoleId",
                table: "TMS_USER",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_TMS_USER_TASK_TaskItemId",
                table: "TMS_USER_TASK",
                column: "TaskItemId");

            migrationBuilder.CreateIndex(
                name: "IX_TMS_USER_TASK_UserId",
                table: "TMS_USER_TASK",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TMS_USER_TASK");

            migrationBuilder.DropTable(
                name: "TMS_TASK_ITEM");

            migrationBuilder.DropTable(
                name: "TMS_USER");

            migrationBuilder.DropTable(
                name: "TMS_ROLE");
        }
    }
}
