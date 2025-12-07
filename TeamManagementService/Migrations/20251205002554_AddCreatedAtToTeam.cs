using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamManagementService.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatedAtToTeam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Teams",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "TeamId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 0, 25, 54, 309, DateTimeKind.Utc).AddTicks(5657));

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "TeamId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 0, 25, 54, 309, DateTimeKind.Utc).AddTicks(5659));

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "TeamId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 0, 25, 54, 309, DateTimeKind.Utc).AddTicks(5660));

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "TeamId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 0, 25, 54, 309, DateTimeKind.Utc).AddTicks(5661));

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "TeamId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 0, 25, 54, 309, DateTimeKind.Utc).AddTicks(5662));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Teams");
        }
    }
}
