﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebCamposDeportivos_V1._2.Data;

#nullable disable

namespace WebCamposDeportivos_V1._2.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebCamposDeportivos_V1._2.Models.Canchas", b =>
                {
                    b.Property<int>("id_cancha")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id_cancha"));

                    b.Property<double>("costoPorHora")
                        .HasColumnType("float");

                    b.Property<string>("detalle")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("estado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("fechaFinal")
                        .HasColumnType("date");

                    b.Property<DateTime>("fechaInicio")
                        .HasColumnType("date");

                    b.Property<TimeSpan>("horaApertura")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("horaCierre")
                        .HasColumnType("time");

                    b.Property<int>("id_deporte")
                        .HasColumnType("int");

                    b.Property<int>("id_empresa")
                        .HasColumnType("int");

                    b.HasKey("id_cancha");

                    b.HasIndex("id_deporte");

                    b.HasIndex("id_empresa");

                    b.ToTable("Canchas");
                });

            modelBuilder.Entity("WebCamposDeportivos_V1._2.Models.Deportes", b =>
                {
                    b.Property<int>("id_deporte")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id_deporte"));

                    b.Property<string>("Descripcion")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<bool>("estado")
                        .HasColumnType("bit");

                    b.HasKey("id_deporte");

                    b.ToTable("Deportes");
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

                    b.Property<string>("nombre")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("id_empresa");

                    b.ToTable("Empresas");
                });

            modelBuilder.Entity("WebCamposDeportivos_V1._2.Models.Pago", b =>
                {
                    b.Property<int>("id_pago")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id_pago"));

                    b.Property<string>("Descripcion")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<bool>("estado")
                        .HasColumnType("bit");

                    b.HasKey("id_pago");

                    b.ToTable("Pagos");
                });

            modelBuilder.Entity("WebCamposDeportivos_V1._2.Models.Reservas", b =>
                {
                    b.Property<int>("id_reserva")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id_reserva"));

                    b.Property<int>("CanchaID")
                        .HasColumnType("int");

                    b.Property<int>("ClienteID")
                        .HasColumnType("int");

                    b.Property<int>("PagoID")
                        .HasColumnType("int");

                    b.Property<DateTime>("fechaReserva")
                        .HasColumnType("date");

                    b.Property<TimeSpan>("horaReserva")
                        .HasColumnType("time");

                    b.Property<double>("total")
                        .HasColumnType("float");

                    b.HasKey("id_reserva");

                    b.HasIndex("CanchaID");

                    b.HasIndex("ClienteID");

                    b.HasIndex("PagoID");

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
                    b.HasOne("WebCamposDeportivos_V1._2.Models.Deportes", "Deporte")
                        .WithMany("Add_Cancha")
                        .HasForeignKey("id_deporte")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebCamposDeportivos_V1._2.Models.Empresas", "Empresa")
                        .WithMany("Add_Cancha")
                        .HasForeignKey("id_empresa")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Deporte");

                    b.Navigation("Empresa");
                });

            modelBuilder.Entity("WebCamposDeportivos_V1._2.Models.Reservas", b =>
                {
                    b.HasOne("WebCamposDeportivos_V1._2.Models.Canchas", "Cancha")
                        .WithMany("Add_Reserva")
                        .HasForeignKey("CanchaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebCamposDeportivos_V1._2.Models.Usuarios", "Cliente")
                        .WithMany("Add_Reserva")
                        .HasForeignKey("ClienteID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebCamposDeportivos_V1._2.Models.Pago", "Pago")
                        .WithMany("Add_Reserva")
                        .HasForeignKey("PagoID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cancha");

                    b.Navigation("Cliente");

                    b.Navigation("Pago");
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

            modelBuilder.Entity("WebCamposDeportivos_V1._2.Models.Deportes", b =>
                {
                    b.Navigation("Add_Cancha");
                });

            modelBuilder.Entity("WebCamposDeportivos_V1._2.Models.Empresas", b =>
                {
                    b.Navigation("Add_Cancha");

                    b.Navigation("Add_EmpresaUsuario");
                });

            modelBuilder.Entity("WebCamposDeportivos_V1._2.Models.Pago", b =>
                {
                    b.Navigation("Add_Reserva");
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