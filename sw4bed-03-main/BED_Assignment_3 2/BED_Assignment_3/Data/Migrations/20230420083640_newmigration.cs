using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BED_Assignment_3.Data.Migrations
{
    /// <inheritdoc />
    public partial class newmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoomNr",
                table: "GuestsExpected");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "GuestsExpected",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "GuestCheckIns",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "GuestsExpected");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "GuestCheckIns");

            migrationBuilder.AddColumn<int>(
                name: "RoomNr",
                table: "GuestsExpected",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
