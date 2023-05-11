using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManagerServer.Migrations
{
    /// <inheritdoc />
    public partial class relationshipchange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SmallHoldingUserEntities");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SmallHoldingUserEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SmallHoldingId = table.Column<int>(type: "int", nullable: false),
                    Decription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmallHoldingUserEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SmallHoldingUserEntities_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SmallHoldingUserEntities_SmallHoldingEntities_SmallHoldingId",
                        column: x => x.SmallHoldingId,
                        principalTable: "SmallHoldingEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SmallHoldingUserEntities_AppUserId",
                table: "SmallHoldingUserEntities",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SmallHoldingUserEntities_SmallHoldingId",
                table: "SmallHoldingUserEntities",
                column: "SmallHoldingId");
        }
    }
}
