using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "TMS_TASK_ITEM",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TMS_TASK_ITEM_ProjectId",
                table: "TMS_TASK_ITEM",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_TMS_TASK_ITEM_TMS_PROJECT_ProjectId",
                table: "TMS_TASK_ITEM",
                column: "ProjectId",
                principalTable: "TMS_PROJECT",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TMS_TASK_ITEM_TMS_PROJECT_ProjectId",
                table: "TMS_TASK_ITEM");

            migrationBuilder.DropIndex(
                name: "IX_TMS_TASK_ITEM_ProjectId",
                table: "TMS_TASK_ITEM");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "TMS_TASK_ITEM");
        }
    }
}
