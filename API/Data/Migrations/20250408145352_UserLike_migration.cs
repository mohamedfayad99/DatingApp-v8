using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserLike_migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LikedUsers",
                columns: table => new
                {
                    sourceuserId = table.Column<int>(type: "INTEGER", nullable: false),
                    targetuserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikedUsers", x => new { x.sourceuserId, x.targetuserId });
                    table.ForeignKey(
                        name: "FK_LikedUsers_Users_sourceuserId",
                        column: x => x.sourceuserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LikedUsers_Users_targetuserId",
                        column: x => x.targetuserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LikedUsers_targetuserId",
                table: "LikedUsers",
                column: "targetuserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LikedUsers");
        }
    }
}
