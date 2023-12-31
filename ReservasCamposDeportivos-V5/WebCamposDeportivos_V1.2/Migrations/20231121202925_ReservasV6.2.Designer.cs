﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebCamposDeportivos_V1._2.Data;

#nullable disable

namespace WebCamposDeportivos_V1._2.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20231121202925_ReservasV6.2")]
    partial class ReservasV62
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebCamposDeportivos_V1._2.Models.Canchas", b =>
                {
                    b.Property<int>("id_cancha")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id_cancha"));

                    b.Property<string>("UrlImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("costoDia")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("costoNoche")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("deporte")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("detalle")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("id_empresa")
                        .HasColumnType("int");

                    b.HasKey("id_cancha");

                    b.HasIndex("id_empresa");

                    b.ToTable("Canchas");
                });

            modelBuilder.Entity("WebCamposDeportivos_V1._2.Models.Empresas", b =>
                {
                    b.Property<int>("id_empresa")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id_empresa"));

                    b.Property<string>("celular")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<string>("correo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<TimeSpan>("horaApertura")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("horaCierre")
                        .HasColumnType("time");

                    b.Property<string>("nombre")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("id_empresa");

                    b.ToTable("Empresas");
                });

            modelBuilder.Entity("WebCamposDeportivos_V1._2.Models.MediosPago", b =>
                {
                    b.Property<int>("id_pagos")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id_pagos"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("id_empresa")
                        .HasColumnType("int");

                    b.HasKey("id_pagos");

                    b.HasIndex("id_empresa");

                    b.ToTable("Pagos");
                });

            modelBuilder.Entity("WebCamposDeportivos_V1._2.Models.Reservas", b =>
                {
                    b.Property<int>("id_reserva")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id_reserva"));

                    b.Property<int>("CampoID")
                        .HasColumnType("int");

                    b.Property<int>("ClienteID")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("date");

                    b.Property<TimeSpan?>("TiempoConfirmacion")
                        .HasColumnType("time");

                    b.Property<string>("estado")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("fechaReserva")
                        .HasColumnType("date");

                    b.Property<TimeSpan>("horaReserva")
                        .HasColumnType("time");

                    b.Property<int>("numeroHoras")
                        .HasColumnType("int");

                    b.Property<decimal>("total")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("id_reserva");

                    b.HasIndex("CampoID");

                    b.HasIndex("ClienteID");

                    b.ToTable("Reservas");
                });

            modelBuilder.Entity("WebCamposDeportivos_V1._2.Models.Rol", b =>
                {
                    b.Property<int>("RolID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RolID"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("RolID");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("WebCamposDeportivos_V1._2.Models.User_Empresa", b =>
                {
                    b.Property<int>("User_EmpresaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("User_EmpresaID"));

                    b.Property<int>("id_empresa")
                        .HasColumnType("int");

                    b.Property<int>("id_usuario")
                        .HasColumnType("int");

                    b.HasKey("User_EmpresaID");

                    b.HasIndex("id_empresa");

                    b.HasIndex("id_usuario");

                    b.ToTable("User_Empresa");
                });

            modelBuilder.Entity("WebCamposDeportivos_V1._2.Models.Usuarios", b =>
                {
                    b.Property<int>("id_usuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id_usuario"));

                    b.Property<string>("apellidos")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("celular")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("nvarchar(9)");

                    b.Property<string>("correo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("documento")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<bool>("estado")
                        .HasColumnType("bit");

                    b.Property<int>("id_rol")
                        .HasColumnType("int");

                    b.Property<string>("nombres")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("pass")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("tipoDocumento")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("usuario")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("id_usuario");

                    b.HasIndex("id_rol");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("WebCamposDeportivos_V1._2.Models.Canchas", b =>
                {
                    b.HasOne("WebCamposDeportivos_V1._2.Models.Empresas", "Empresa")
                        .WithMany("Add_Cancha")
                        .HasForeignKey("id_empresa")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Empresa");
                });

            modelBuilder.Entity("WebCamposDeportivos_V1._2.Models.MediosPago", b =>
                {
                    b.HasOne("WebCamposDeportivos_V1._2.Models.Empresas", "Empresas")
                        .WithMany("Add_MedioPago")
                        .HasForeignKey("id_empresa")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Empresas");
                });

            modelBuilder.Entity("WebCamposDeportivos_V1._2.Models.Reservas", b =>
                {
                    b.HasOne("WebCamposDeportivos_V1._2.Models.Canchas", "Campo")
                        .WithMany("Add_Reserva")
                        .HasForeignKey("CampoID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebCamposDeportivos_V1._2.Models.Usuarios", "Cliente")
                        .WithMany("Add_Reserva")
                        .HasForeignKey("ClienteID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Campo");

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("WebCamposDeportivos_V1._2.Models.User_Empresa", b =>
                {
                    b.HasOne("WebCamposDeportivos_V1._2.Models.Empresas", "Empresa")
                        .WithMany("Add_EmpresaUsuario")
                        .HasForeignKey("id_empresa")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebCamposDeportivos_V1._2.Models.Usuarios", "Usuario")
                        .WithMany("Add_EmpresaUsuario")
                        .HasForeignKey("id_usuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Empresa");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("WebCamposDeportivos_V1._2.Models.Usuarios", b =>
                {
                    b.HasOne("WebCamposDeportivos_V1._2.Models.Rol", "Rol")
                        .WithMany("Add_Usuario")
                        .HasForeignKey("id_rol")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rol");
                });

            modelBuilder.Entity("WebCamposDeportivos_V1._2.Models.Canchas", b =>
                {
                    b.Navigation("Add_Reserva");
                });

            modelBuilder.Entity("WebCamposDeportivos_V1._2.Models.Empresas", b =>
                {
                    b.Navigation("Add_Cancha");

                    b.Navigation("Add_EmpresaUsuario");

                    b.Navigation("Add_MedioPago");
                });

            modelBuilder.Entity("WebCamposDeportivos_V1._2.Models.Rol", b =>
                {
                    b.Navigation("Add_Usuario");
                });

            modelBuilder.Entity("WebCamposDeportivos_V1._2.Models.Usuarios", b =>
                {
                    b.Navigation("Add_EmpresaUsuario");

                    b.Navigation("Add_Reserva");
                });
#pragma warning restore 612, 618
        }
    }
}
