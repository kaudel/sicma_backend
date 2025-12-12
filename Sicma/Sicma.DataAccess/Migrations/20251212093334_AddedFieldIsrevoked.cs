using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sicma.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedFieldIsrevoked : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRevoked",
                table: "TokenHistory",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRevoked",
                table: "TokenHistory");
        }
    }
}
