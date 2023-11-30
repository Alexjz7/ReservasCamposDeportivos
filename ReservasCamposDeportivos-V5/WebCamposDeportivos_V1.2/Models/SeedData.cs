using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
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
                    new Rol { Descripcion = "Supervisor" },
                    new Rol { Descripcion = "Administrador" },
                    new Rol { Descripcion = "Cliente" }
                 );
                db.SaveChanges();
            }
        }

        public static void AddSupervisor(IServiceProvider serviceProvider)
        {
            using (var context = new AppDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<AppDbContext>>()))
            {
                //Buscar si hay categorías
                if (context.Usuarios.Any())
                {
                    return;
                }

                context.Usuarios.AddRange(
                    new Usuarios
                    {
                        id_rol = 1,
                        usuario = "SoundWolf",
                        //pass = "admin$123",
                        pass = Encriptar("admin$123"),
                        nombres = "Fabricio Nicolás",
                        apellidos = "Gutiérrez Mendoza",
                        tipoDocumento = "DNI",
                        documento = "83214756",
                        correo = "Fabr23@outlook.com",
                        celular = "943286177",
                        estado = true
                    }
                );

                context.SaveChanges();
            }
        }
        public static string Encriptar(string texto)
        {
            StringBuilder sb = new StringBuilder();
            using (SHA256 hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));
                foreach (byte b in result)
                {
                    sb.Append(b.ToString("x2"));
                }
            }
            return sb.ToString();
        }
    }
}
