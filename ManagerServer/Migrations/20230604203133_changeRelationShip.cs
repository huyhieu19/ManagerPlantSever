using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManagerServer.Migrations
{
    /// <inheritdoc />
    public partial class changeRelationShip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeviceEntities_ZoneEntities_ZoneId",
                table: "DeviceEntities");

            migrationBuilder.DropIndex(
                name: "IX_DeviceEntities_ZoneId",
                table: "DeviceEntities");

            migrationBuilder.AddColumn<int>(
                name: "ZoneEntityId",
                table: "DeviceEntities",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeviceEntities_ZoneEntityId",
                table: "DeviceEntities",
                column: "ZoneEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceEntities_ZoneEntities_ZoneEntityId",
                table: "DeviceEntities",
                column: "ZoneEntityId",
                principalTable: "ZoneEntities",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeviceEntities_ZoneEntities_ZoneEntityId",
                table: "DeviceEntities");

            migrationBuilder.DropIndex(
                name: "IX_DeviceEntities_ZoneEntityId",
                table: "DeviceEntities");

            migrationBuilder.DropColumn(
                name: "ZoneEntityId",
                table: "DeviceEntities");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceEntities_ZoneId",
                table: "DeviceEntities",
                column: "ZoneId",
                unique: true,
                filter: "[ZoneId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceEntities_ZoneEntities_ZoneId",
                table: "DeviceEntities",
                column: "ZoneId",
                principalTable: "ZoneEntities",
                principalColumn: "Id");
        }
    }
}
