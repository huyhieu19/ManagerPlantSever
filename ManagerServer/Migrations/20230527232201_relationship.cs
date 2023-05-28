using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManagerServer.Migrations
{
    /// <inheritdoc />
    public partial class relationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DeviceEntities_ZoneId",
                table: "DeviceEntities");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceEntities_ZoneId",
                table: "DeviceEntities",
                column: "ZoneId",
                unique: true,
                filter: "[ZoneId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DeviceEntities_ZoneId",
                table: "DeviceEntities");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceEntities_ZoneId",
                table: "DeviceEntities",
                column: "ZoneId");
        }
    }
}
