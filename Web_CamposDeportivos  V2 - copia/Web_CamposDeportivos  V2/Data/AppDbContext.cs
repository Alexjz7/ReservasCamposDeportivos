using Microsoft.EntityFrameworkCore;
using Web_CamposDeportivos__V2.Models;

namespace Web_CamposDeportivos__V2.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Usuarios> Usuarios{ get; set; }
        public DbSet<Gerentes> Gerentes { get; set; }
        public DbSet<Canchas> Canchas { get; set; }
        public DbSet<Reservas> Reservas { get; set; }
    }
}
