using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManagerServer.Migrations
{
    /// <inheritdoc />
    public partial class indddd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ZoneEntities_FarmEntities_FarmId",
                table: "ZoneEntities");

            migrationBuilder.DropIndex(
                name: "IX_ZoneEntities_FarmId",
                table: "ZoneEntities");

            migrationBuilder.AddColumn<int>(
                name: "FarmEntityId",
                table: "ZoneEntities",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ZoneEntities_FarmEntityId",
                table: "ZoneEntities",
                column: "FarmEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_ZoneEntities_FarmEntities_FarmEntityId",
                table: "ZoneEntities",
                column: "FarmEntityId",
                principalTable: "FarmEntities",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ZoneEntities_FarmEntities_FarmEntityId",
                table: "ZoneEntities");

            migrationBuilder.DropIndex(
                name: "IX_ZoneEntities_FarmEntityId",
                table: "ZoneEntities");

            migrationBuilder.DropColumn(
                name: "FarmEntityId",
                table: "ZoneEntities");

            migrationBuilder.CreateIndex(
                name: "IX_ZoneEntities_FarmId",
                table: "ZoneEntities",
                column: "FarmId");

            migrationBuilder.AddForeignKey(
                name: "FK_ZoneEntities_FarmEntities_FarmId",
                table: "ZoneEntities",
                column: "FarmId",
                principalTable: "FarmEntities",
                principalColumn: "Id");
        }
    }
}
