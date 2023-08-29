using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using ReservasCampoDeportivo.Models;
using System.Security.Cryptography;
using System.Text;


namespace ReservasCampoDeportivo.Controllers
{
    public class AccesoController : Controller
    {
        static string cadena = "Data Source=(local);Catalog=DB_ReservaCampoDeportivo;Integrated Security=True";


        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registrar(Usuarios oUsuarios)
        {
            bool registrado;
            string? mensaje;

            if (oUsuarios.pass == oUsuarios.Confirmarpass)
            {
                oUsuarios.pass = EncriptarConLongitudMaxima(oUsuarios.pass, 50);
            }
            else
            {
                ViewData["Mensaje"] = "Las contraseñas no coinsiden";
                return View();
            }

            using (SqlConnection con = new SqlConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_RegistrarUsuario", con);
                cmd.Parameters.AddWithValue("usuario",oUsuarios.usuario);
                cmd.Parameters.AddWithValue("pass", oUsuarios.pass);
                cmd.Parameters.AddWithValue("nombres", oUsuarios.nombres);
                cmd.Parameters.AddWithValue("apellidos", oUsuarios.apellidos);
                cmd.Parameters.AddWithValue("tipoDocumento", oUsuarios.tipoDocumento);
                cmd.Parameters.AddWithValue("documento", oUsuarios.documento);
                cmd.Parameters.AddWithValue("correo", oUsuarios.correo);
                cmd.Parameters.AddWithValue("celular", oUsuarios.celular);

                cmd.Parameters.Add("registrado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("mensaje", SqlDbType.VarChar,30).Direction = ParameterDirection.Output;

                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                cmd.ExecuteNonQuery();

                registrado = Convert.ToBoolean(cmd.Parameters["registrado"].Value);
                mensaje = cmd.Parameters["mensaje"].Value.ToString();

            }
            ViewData["mensaje"] = mensaje;
            if (registrado)
            {
                return RedirectToAction("Login", "Acceso");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public IActionResult Login(Usuarios oUsuarios)
        {
            oUsuarios.pass = EncriptarConLongitudMaxima(oUsuarios.pass, 50);
            using (SqlConnection con = new SqlConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_ValidarUsuario", con);
                cmd.Parameters.AddWithValue("correo", oUsuarios.usuario);
                cmd.Parameters.AddWithValue("pass", oUsuarios.pass);                
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                oUsuarios.id_usuario=Convert.ToInt32(cmd.ExecuteScalar().ToString());
            }
            if (oUsuarios.id_usuario != 0)
            {
                //Session["usuario"] = oUsuarios;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["mensaje"] = "Usuario no encontrado";
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
