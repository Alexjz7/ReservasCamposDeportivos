﻿using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebCamposDeportivos_V1._2.Data;
using WebCamposDeportivos_V1._2.Models;

namespace WebCamposDeportivos_V1._2.Controllers
{
    public class AccessController : Controller
    {
        private readonly AppDbContext _context;

        public AccessController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Login(Usuarios user)
        {
            if (user.usuario != null && user.pass != null)
            {
                //Validar los credenciales
                var _user = validarCredenciales(user.usuario, user.pass);

                if (_user != null)
                {
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, _user.nombres+ " " + _user.apellidos),
                    new Claim(ClaimTypes.Email, _user.correo),
                    new Claim(ClaimTypes.GivenName, _user.usuario),
                    new Claim(ClaimTypes.MobilePhone, _user.celular),
                    new Claim(ClaimTypes.GivenName, _user.tipoDocumento),
                    new Claim(ClaimTypes.SerialNumber, _user.documento),
                    new Claim(ClaimTypes.Role, _user.Rol.Descripcion)
                };

                    var claimsIdentity = new ClaimsIdentity(claims,
                                                            CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                                    new ClaimsPrincipal(claimsIdentity));
                    return RedirectToAction("Index", "Reservas");
                }
                ViewData["Mensaje"] = "El usuario y/o la contraseña son incorrectos.";
            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Access");
        }

        [HttpGet]
        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registrar(Usuarios user)
        {
            if (user.pass != null)
            {
                user.pass = Encriptar(user.pass);
                user.id_rol = 3;
                if (ModelState.IsValid)
                {
                    _context.Usuarios.Add(user);
                    _context.SaveChanges();
                    ViewData["Mensaje"] = "Usuario registrado exitosamente";
                    return RedirectToAction("Index", "Reservas");
                }
            }
            return View();
        }

        private Usuarios validarCredenciales(string alias, string clave)
        {
            clave = Encriptar(clave);
            var tipo_usuario = _context.Usuarios.Include(r => r.Rol)
                                    .Where(u => u.usuario == alias)
                                    .Where(u => u.pass == clave)
                                    .FirstOrDefault();
            return tipo_usuario;
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

        [HttpGet]
        public IActionResult AdminRegistro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AdminRegistro(Usuarios user)
        {
            user.pass = Encriptar(user.pass);
            user.id_rol = 2;
            if (ModelState.IsValid)
            {
                _context.Usuarios.Add(user);
                _context.SaveChanges();
                ViewData["Mensaje"] = "Usuario registrado exitosamente";
                return RedirectToAction("Login", "Access");
            }
            return View();
        }

        [HttpGet]
        public IActionResult SesionOperario()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SesionOperario(Usuarios user)
        {
            var _user = validarOrganizador(user.usuario, user.pass);

            if (_user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, _user.nombres+ " " + _user.apellidos),
                    new Claim(ClaimTypes.Email, _user.correo),
                    new Claim(ClaimTypes.GivenName, _user.usuario),
                    new Claim(ClaimTypes.MobilePhone, _user.celular),
                    new Claim(ClaimTypes.GivenName, _user.tipoDocumento),
                    new Claim(ClaimTypes.SerialNumber, _user.documento),
                    new Claim(ClaimTypes.Role, _user.Rol.Descripcion)
                };

                var claimsIdentity = new ClaimsIdentity(claims,
                                                        CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                                new ClaimsPrincipal(claimsIdentity));
                return RedirectToAction("Index", "Reservas");
            }

            return View();
        }

        private Usuarios validarOrganizador(string alias, string clave)
        {
            var tipo_usuario = _context.Usuarios.Include(r => r.Rol)
                                    .Where(u => u.usuario == alias)
                                    .Where(u => u.pass == clave)
                                    .FirstOrDefault();
            return tipo_usuario;
        }
    }
}