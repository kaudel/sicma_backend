using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sicma.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedRelationExercise_TrainingType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TrainingTypeId",
                table: "Exercise",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Exercise_TrainingTypeId",
                table: "Exercise",
                column: "TrainingTypeId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Exercise_TrainingTypes_TrainingTypeId",
                table: "Exercise",
                column: "TrainingTypeId",
                principalTable: "TrainingTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercise_TrainingTypes_TrainingTypeId",
                table: "Exercise");

            migrationBuilder.DropIndex(
                name: "IX_Exercise_TrainingTypeId",
                table: "Exercise");

            migrationBuilder.AlterColumn<string>(
                name: "TrainingTypeId",
                table: "Exercise",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
