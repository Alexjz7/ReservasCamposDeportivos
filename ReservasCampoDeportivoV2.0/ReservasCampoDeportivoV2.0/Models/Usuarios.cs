using System.ComponentModel.DataAnnotations;

namespace ReservasCampoDeportivo.Models
{
    public class Usuarios
    {
        [Key]
        public int id_usuario { get; set; }

        [Required(ErrorMessage = "Se requiere ingresar un nombre de usuario")]
        [StringLength(30)]
        public string? usuario { get; set; }

        [Required(ErrorMessage = "Se requiere ingresar una contraseña")]
        [StringLength(50)]
        public string? pass { get; set; }

        [Required(ErrorMessage = "Se requiere ingresar nombre(s)")]
        [StringLength(30)]
        public string? nombres { get; set; }

        [Required(ErrorMessage = "Se requiere ingresar apellidos")]
        [StringLength(30)]
        public string? apellidos { get; set; }

        [Required(ErrorMessage = "Se requiere ingresar el tipo de documento")]
        [StringLength(15)]
        public string? tipoDocumento { get; set; }

        [Required(ErrorMessage = "Se requiere ingresar el N° documento")]
        [StringLength(20)]
        public string? documento { get; set; }

        [Required(ErrorMessage = "Se requiere ingresar un correo")]
        [StringLength(50)]
        public string? correo { get; set; }

        [Required(ErrorMessage = "Se requiere ingresar un celular")]
        [StringLength(13)]
        public string? celular { get; set; }

        public bool validacion { get; set; }
    }
}
