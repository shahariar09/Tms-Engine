using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_TMS_PROJECT_USER_remove_Id : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TMS_PROJECT_USER_TMS_PROJECT_ProjectId",
                table: "TMS_PROJECT_USER");

            migrationBuilder.DropForeignKey(
                name: "FK_TMS_PROJECT_USER_TMS_USER_UserId",
                table: "TMS_PROJECT_USER");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TMS_PROJECT_USER",
                table: "TMS_PROJECT_USER");

            migrationBuilder.DropIndex(
                name: "IX_TMS_PROJECT_USER_ProjectId",
                table: "TMS_PROJECT_USER");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TMS_PROJECT_USER");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TMS_PROJECT_USER",
                table: "TMS_PROJECT_USER",
                columns: new[] { "ProjectId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TMS_PROJECT_USER_TMS_PROJECT_ProjectId",
                table: "TMS_PROJECT_USER",
                column: "ProjectId",
                principalTable: "TMS_PROJECT",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TMS_PROJECT_USER_TMS_USER_UserId",
                table: "TMS_PROJECT_USER",
                column: "UserId",
                principalTable: "TMS_USER",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TMS_PROJECT_USER_TMS_PROJECT_ProjectId",
                table: "TMS_PROJECT_USER");

            migrationBuilder.DropForeignKey(
                name: "FK_TMS_PROJECT_USER_TMS_USER_UserId",
                table: "TMS_PROJECT_USER");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TMS_PROJECT_USER",
                table: "TMS_PROJECT_USER");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "TMS_PROJECT_USER",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TMS_PROJECT_USER",
                table: "TMS_PROJECT_USER",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TMS_PROJECT_USER_ProjectId",
                table: "TMS_PROJECT_USER",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_TMS_PROJECT_USER_TMS_PROJECT_ProjectId",
                table: "TMS_PROJECT_USER",
                column: "ProjectId",
                principalTable: "TMS_PROJECT",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TMS_PROJECT_USER_TMS_USER_UserId",
                table: "TMS_PROJECT_USER",
                column: "UserId",
                principalTable: "TMS_USER",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
