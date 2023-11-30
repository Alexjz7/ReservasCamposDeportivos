using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebCamposDeportivos_V1._2.Migrations
{
    /// <inheritdoc />
    public partial class ReservasV61 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TiempoConfirmacion",
                table: "Reservas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "TiempoConfirmacion",
                table: "Reservas",
                type: "time",
                nullable: true);
        }
    }
}
