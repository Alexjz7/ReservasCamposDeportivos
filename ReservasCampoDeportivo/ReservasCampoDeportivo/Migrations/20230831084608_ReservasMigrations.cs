using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReservasCampoDeportivo.Migrations
{
    /// <inheritdoc />
    public partial class ReservasMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gerentes",
                columns: table => new
                {
                    id_gerente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    usuario = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    pass = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    nombres = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    apellidos = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    tipoDocumento = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    documento = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    correo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    celular = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    validacion = table.Column<bool>(type: "bit", nullable: false),
                    nombreCampo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    horaApertura = table.Column<TimeSpan>(type: "time", nullable: false),
                    horaCierre = table.Column<TimeSpan>(type: "time", nullable: false),
                    costoPorHora = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gerentes", x => x.id_gerente);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    id_usuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    usuario = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    pass = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Confirmarpass = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    nombres = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    apellidos = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    tipoDocumento = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    documento = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    correo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    celular = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    validacion = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.id_usuario);
                });

            migrationBuilder.CreateTable(
                name: "Canchas",
                columns: table => new
                {
                    id_cancha = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_gerente = table.Column<int>(type: "int", nullable: false),
                    deporte = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    disponible = table.Column<bool>(type: "bit", nullable: false),
                    cantidadCanchas = table.Column<int>(type: "int", nullable: false),
                    reservada = table.Column<bool>(type: "bit", nullable: false),
                    Gerenteid_gerente = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Canchas", x => x.id_cancha);
                    table.ForeignKey(
                        name: "FK_Canchas_Gerentes_Gerenteid_gerente",
                        column: x => x.Gerenteid_gerente,
                        principalTable: "Gerentes",
                        principalColumn: "id_gerente");
                });

            migrationBuilder.CreateTable(
                name: "Reservas",
                columns: table => new
                {
                    id_reserva = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_cancha = table.Column<int>(type: "int", nullable: false),
                    id_cliente = table.Column<int>(type: "int", nullable: false),
                    fechaReserva = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Canchaid_cancha = table.Column<int>(type: "int", nullable: true),
                    Clienteid_usuario = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservas", x => x.id_reserva);
                    table.ForeignKey(
                        name: "FK_Reservas_Canchas_Canchaid_cancha",
                        column: x => x.Canchaid_cancha,
                        principalTable: "Canchas",
                        principalColumn: "id_cancha");
                    table.ForeignKey(
                        name: "FK_Reservas_Usuarios_Clienteid_usuario",
                        column: x => x.Clienteid_usuario,
                        principalTable: "Usuarios",
                        principalColumn: "id_usuario");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Canchas_Gerenteid_gerente",
                table: "Canchas",
                column: "Gerenteid_gerente");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_Canchaid_cancha",
                table: "Reservas",
                column: "Canchaid_cancha");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_Clienteid_usuario",
                table: "Reservas",
                column: "Clienteid_usuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservas");

            migrationBuilder.DropTable(
                name: "Canchas");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Gerentes");
        }
    }
}
