using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManagerServer.Migrations
{
    /// <inheritdoc />
    public partial class fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Decription",
                table: "SmallHoldingEntities",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Decription",
                table: "SmallHoldingEntities");
        }
    }
}
