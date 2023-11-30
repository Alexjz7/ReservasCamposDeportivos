using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebCamposDeportivos_V1._2.Migrations
{
    /// <inheritdoc />
    public partial class ReservasV5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Empresas",
                columns: table => new
                {
                    id_empresa = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    correo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    celular = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    horaApertura = table.Column<TimeSpan>(type: "time", nullable: false),
                    horaCierre = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresas", x => x.id_empresa);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RolID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RolID);
                });

            migrationBuilder.CreateTable(
                name: "Canchas",
                columns: table => new
                {
                    id_cancha = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_empresa = table.Column<int>(type: "int", nullable: false),
                    deporte = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    detalle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    costoDia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    costoNoche = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UrlImage = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Canchas", x => x.id_cancha);
                    table.ForeignKey(
                        name: "FK_Canchas_Empresas_id_empresa",
                        column: x => x.id_empresa,
                        principalTable: "Empresas",
                        principalColumn: "id_empresa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pagos",
                columns: table => new
                {
                    id_pagos = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_empresa = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagos", x => x.id_pagos);
                    table.ForeignKey(
                        name: "FK_Pagos_Empresas_id_empresa",
                        column: x => x.id_empresa,
                        principalTable: "Empresas",
                        principalColumn: "id_empresa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    id_usuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_rol = table.Column<int>(type: "int", nullable: false),
                    usuario = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    pass = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nombres = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    apellidos = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    tipoDocumento = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    documento = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    correo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    celular = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.id_usuario);
                    table.ForeignKey(
                        name: "FK_Usuarios_Roles_id_rol",
                        column: x => x.id_rol,
                        principalTable: "Roles",
                        principalColumn: "RolID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservas",
                columns: table => new
                {
                    id_reserva = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienteID = table.Column<int>(type: "int", nullable: false),
                    CampoID = table.Column<int>(type: "int", nullable: false),
                    fechaReserva = table.Column<DateTime>(type: "date", nullable: false),
                    horaReserva = table.Column<TimeSpan>(type: "time", nullable: false),
                    numeroHoras = table.Column<int>(type: "int", nullable: false),
                    total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    estado = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservas", x => x.id_reserva);
                    table.ForeignKey(
                        name: "FK_Reservas_Canchas_CampoID",
                        column: x => x.CampoID,
                        principalTable: "Canchas",
                        principalColumn: "id_cancha",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservas_Usuarios_ClienteID",
                        column: x => x.ClienteID,
                        principalTable: "Usuarios",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User_Empresa",
                columns: table => new
                {
                    User_EmpresaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_usuario = table.Column<int>(type: "int", nullable: false),
                    id_empresa = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Empresa", x => x.User_EmpresaID);
                    table.ForeignKey(
                        name: "FK_User_Empresa_Empresas_id_empresa",
                        column: x => x.id_empresa,
                        principalTable: "Empresas",
                        principalColumn: "id_empresa",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_Empresa_Usuarios_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "Usuarios",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Canchas_id_empresa",
                table: "Canchas",
                column: "id_empresa");

            migrationBuilder.CreateIndex(
                name: "IX_Pagos_id_empresa",
                table: "Pagos",
                column: "id_empresa");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_CampoID",
                table: "Reservas",
                column: "CampoID");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_ClienteID",
                table: "Reservas",
                column: "ClienteID");

            migrationBuilder.CreateIndex(
                name: "IX_User_Empresa_id_empresa",
                table: "User_Empresa",
                column: "id_empresa");

            migrationBuilder.CreateIndex(
                name: "IX_User_Empresa_id_usuario",
                table: "User_Empresa",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_id_rol",
                table: "Usuarios",
                column: "id_rol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pagos");

            migrationBuilder.DropTable(
                name: "Reservas");

            migrationBuilder.DropTable(
                name: "User_Empresa");

            migrationBuilder.DropTable(
                name: "Canchas");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Empresas");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
