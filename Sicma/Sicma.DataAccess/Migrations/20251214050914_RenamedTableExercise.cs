using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sicma.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RenamedTableExercise : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Exercise");

            migrationBuilder.CreateTable(
                name: "PracticeConfigs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OperationConfigId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TrainingTypeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedUserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PracticeConfigs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PracticeConfigs_OperationConfigs_OperationConfigId",
                        column: x => x.OperationConfigId,
                        principalTable: "OperationConfigs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PracticeConfigs_TrainingTypes_TrainingTypeId",
                        column: x => x.TrainingTypeId,
                        principalTable: "TrainingTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PracticeConfigs_OperationConfigId",
                table: "PracticeConfigs",
                column: "OperationConfigId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PracticeConfigs_TrainingTypeId",
                table: "PracticeConfigs",
                column: "TrainingTypeId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PracticeConfigs");

            migrationBuilder.CreateTable(
                name: "Exercise",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OperationConfigId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TrainingTypeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercise", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exercise_OperationConfigs_OperationConfigId",
                        column: x => x.OperationConfigId,
                        principalTable: "OperationConfigs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Exercise_TrainingTypes_TrainingTypeId",
                        column: x => x.TrainingTypeId,
                        principalTable: "TrainingTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exercise_OperationConfigId",
                table: "Exercise",
                column: "OperationConfigId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Exercise_TrainingTypeId",
                table: "Exercise",
                column: "TrainingTypeId",
                unique: true);
        }
    }
}
