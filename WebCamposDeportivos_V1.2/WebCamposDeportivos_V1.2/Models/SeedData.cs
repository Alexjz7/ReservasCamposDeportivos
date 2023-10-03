using Microsoft.EntityFrameworkCore;
using WebCamposDeportivos_V1._2.Data;

namespace WebCamposDeportivos_V1._2.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var db = new AppDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<AppDbContext>>()))
            {
                //Verificar si ya existe los datos que se van a insertar (Rol)
                if (db.Roles.Any())
                {
                    return; //NO hagas ninguna insersión
                }

                //Si no hay aun información (Tabla vacía)
                db.Roles.AddRange(
                    new Rol
                    {
                        Descripcion = "Operario"
                    },
                    new Rol
                    {
                        Descripcion = "Admin"
                    },
                    new Rol
                    {
                        Descripcion = "Cliente"
                    }
                 );
                db.SaveChanges();
            }
        }
    }
}
