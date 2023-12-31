﻿using Microsoft.EntityFrameworkCore;
using WebCamposDeportivos_V1._2.Models;

namespace WebCamposDeportivos_V1._2.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Usuarios> Usuarios{ get; set; }
        public DbSet<Canchas> Canchas { get; set; }
        public DbSet<Reservas> Reservas { get; set; }
        public DbSet<Empresas> Empresas { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<MediosPago> Pagos { get; set; }
        public DbSet<User_Empresa> User_Empresa { get; set; }
    }
}
