using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeavensWayApi.Migrations
{
    /// <inheritdoc />
    public partial class DataFimEvento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duracao",
                table: "Eventos");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataFim",
                table: "Eventos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataFim",
                table: "Eventos");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Duracao",
                table: "Eventos",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}
