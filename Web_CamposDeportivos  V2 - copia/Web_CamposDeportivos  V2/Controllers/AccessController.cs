using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_CamposDeportivos__V2.Data;
using Web_CamposDeportivos__V2.Models;

namespace Web_CamposDeportivos__V2.Controllers
{
    public class AccessController : Controller
    {
        private readonly AppDbContext _context;

        public AccessController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registrar(Usuarios oUsuarios)
        {
            bool registrado;
            string mensaje;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(string correo, string pass)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.correo == correo && u.pass == pass);

            if (usuario != null)
            {
                // Inicio de sesión exitoso
                // Puedes agregar aquí el código para redirigir a la página deseada
                // Por ejemplo, redirigir a la página de inicio después del inicio de sesión:
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Inicio de sesión fallido
                ViewBag.Error = "Credenciales inválidas";
                return View();
            }
        }

        public static string EncriptarConLongitudMaxima(string input, int longitudMaxima)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);

                string hashString = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

                if (hashString.Length > longitudMaxima)
                {
                    hashString = hashString.Substring(0, longitudMaxima);
                }

                return hashString;
            }
        }
    }
}
