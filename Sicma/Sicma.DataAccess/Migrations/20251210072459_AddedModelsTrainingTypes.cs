using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sicma.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedModelsTrainingTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PracticeLevels");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "OperationConfigs",
                newName: "TypeSimbol");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "OperationConfigs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RegexExpression",
                table: "OperationConfigs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TimePeriodSeconds",
                table: "OperationConfigs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TrainingTypes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedUserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingTypes", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrainingTypes");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "OperationConfigs");

            migrationBuilder.DropColumn(
                name: "RegexExpression",
                table: "OperationConfigs");

            migrationBuilder.DropColumn(
                name: "TimePeriodSeconds",
                table: "OperationConfigs");

            migrationBuilder.RenameColumn(
                name: "TypeSimbol",
                table: "OperationConfigs",
                newName: "Type");

            migrationBuilder.CreateTable(
                name: "PracticeLevels",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PracticeLevels", x => x.Id);
                });
        }
    }
}
