//using Microsoft.EntityFrameworkCore.Migrations;

//#nullable disable

//namespace Tms.Infrastructure.Migrations
//{
//    /// <inheritdoc />
//    public partial class create_TMS_PROJECT_USER : Migration
//    {
//        /// <inheritdoc />
//        protected override void Up(MigrationBuilder migrationBuilder)
//        {


//        }

//        /// <inheritdoc />
//        protected override void Down(MigrationBuilder migrationBuilder)
//        {

//        }
//    }
//}

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
            // Create TMS_PROJECT table if it doesn't already exist (optional)
            //migrationBuilder.CreateTable(
            //    name: "TMS_PROJECT",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        DueDate = table.Column<DateTime>(type: "datetime2", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_TMS_PROJECT", x => x.Id);
            //    });

            // Create TMS_PROJECT_USER table
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

            // Create indexes to optimize queries
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
            // Drop TMS_PROJECT_USER table
            migrationBuilder.DropTable(
                name: "TMS_PROJECT_USER");

            // Drop TMS_PROJECT table if it was created in this migration (optional)
            migrationBuilder.DropTable(
                name: "TMS_PROJECT");
        }
    }
}

