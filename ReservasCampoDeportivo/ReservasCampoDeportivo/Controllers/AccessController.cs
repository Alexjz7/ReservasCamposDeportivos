using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservasCampoDeportivo.Data;
using ReservasCampoDeportivo.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace ReservasCampoDeportivo.Controllers
{
    public class AccesoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccesoController(ApplicationDbContext context)
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
            if (oUsuarios.pass == oUsuarios.Confirmarpass)
            {
                // Registrar el usuario en la base de datos
                _context.Usuarios.Add(oUsuarios);
                await _context.SaveChangesAsync();

                return RedirectToAction("Login");
            }
            else
            {
                ViewData["Mensaje"] = "Las contraseñas no coinciden";
                return View();
            }
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