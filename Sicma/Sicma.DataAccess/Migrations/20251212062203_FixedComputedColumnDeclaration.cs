using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sicma.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FixedComputedColumnDeclaration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "TokenHistory",
                type: "bit",
                nullable: false,
                computedColumnSql: "Case WHEN [ExpirationDate] > GETDATE() THEN CAST(1 AS bit) ELSE CAST(0 AS bit) END",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComputedColumnSql: "Case WHEN [ExpirationDate] > GETDATE() THEN 1 ELSE 0 END");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "TokenHistory",
                type: "bit",
                nullable: false,
                computedColumnSql: "Case WHEN [ExpirationDate] > GETDATE() THEN 1 ELSE 0 END",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComputedColumnSql: "Case WHEN [ExpirationDate] > GETDATE() THEN CAST(1 AS bit) ELSE CAST(0 AS bit) END");
        }
    }
}
