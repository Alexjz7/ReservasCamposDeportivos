using Microsoft.EntityFrameworkCore;
using ReservasCampoDeportivo.Models;

namespace ReservasCampoDeportivo.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Gerentes> Gerentes { get; set; }
        public DbSet<Canchas> Canchas { get; set; }
        public DbSet<Reservas> Reservas { get; set; }

    }
}
