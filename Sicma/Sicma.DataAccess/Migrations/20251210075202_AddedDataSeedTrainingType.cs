using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Sicma.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedDataSeedTrainingType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TrainingTypes",
                columns: new[] { "Id", "CreatedDate", "CreatedUserId", "Description", "IsActive", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { "0af6fbbc-4388-4c4d-a7e0-dff5900b45ca", new DateTime(2025, 12, 10, 7, 45, 12, 665, DateTimeKind.Utc).AddTicks(4318), "1", "Opcion entrenamiento", true, "Entrenamiento", null },
                    { "28c52fce-4774-4a1b-8d7c-ff98dd43ff19", new DateTime(2025, 12, 10, 7, 45, 12, 665, DateTimeKind.Utc).AddTicks(1971), "1", "Opcion torneo", true, "Torneo", null },
                    { "78cfc855-13be-4eb3-9d42-bff6c402f479", new DateTime(2025, 12, 10, 7, 45, 12, 665, DateTimeKind.Utc).AddTicks(4325), "1", "Opcion 1 contra 1", true, "1vs1", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TrainingTypes",
                keyColumn: "Id",
                keyValue: "0af6fbbc-4388-4c4d-a7e0-dff5900b45ca");

            migrationBuilder.DeleteData(
                table: "TrainingTypes",
                keyColumn: "Id",
                keyValue: "28c52fce-4774-4a1b-8d7c-ff98dd43ff19");

            migrationBuilder.DeleteData(
                table: "TrainingTypes",
                keyColumn: "Id",
                keyValue: "78cfc855-13be-4eb3-9d42-bff6c402f479");
        }
    }
}
