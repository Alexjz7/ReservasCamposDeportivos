using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebCamposDeportivos_V1._2.Migrations
{
    /// <inheritdoc />
    public partial class ReservasV54 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "Reservas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "TiempoConfirmacion",
                table: "Reservas",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "Reservas");

            migrationBuilder.DropColumn(
                name: "TiempoConfirmacion",
                table: "Reservas");
        }
    }
}
