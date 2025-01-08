﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixUserTaskRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TMS_USER_TASK_TMS_TASK_ITEM_TaskItemId1",
                table: "TMS_USER_TASK");

            migrationBuilder.DropIndex(
                name: "IX_TMS_USER_TASK_TaskItemId1",
                table: "TMS_USER_TASK");

            migrationBuilder.DropColumn(
                name: "TaskItemId1",
                table: "TMS_USER_TASK");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TaskItemId1",
                table: "TMS_USER_TASK",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TMS_USER_TASK_TaskItemId1",
                table: "TMS_USER_TASK",
                column: "TaskItemId1");

            migrationBuilder.AddForeignKey(
                name: "FK_TMS_USER_TASK_TMS_TASK_ITEM_TaskItemId1",
                table: "TMS_USER_TASK",
                column: "TaskItemId1",
                principalTable: "TMS_TASK_ITEM",
                principalColumn: "Id");
        }
    }
}
