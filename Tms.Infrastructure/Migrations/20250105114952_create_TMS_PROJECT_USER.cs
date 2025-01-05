using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class create_TMS_PROJECT_USER : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TMS_PROJECT_USER",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TMS_PROJECT_USER", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TMS_PROJECT_USER_TMS_PROJECT_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "TMS_PROJECT",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TMS_PROJECT_USER_TMS_USER_UserId",
                        column: x => x.UserId,
                        principalTable: "TMS_USER",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TMS_PROJECT_USER_ProjectId",
                table: "TMS_PROJECT_USER",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TMS_PROJECT_USER_UserId",
                table: "TMS_PROJECT_USER",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TMS_PROJECT_USER");
        }
    }
}
