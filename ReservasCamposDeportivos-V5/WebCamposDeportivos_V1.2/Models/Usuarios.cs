
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebCamposDeportivos_V1._2.Models
{
    public class Usuarios
    {
        [Key]
        public int id_usuario { get; set; }

        [ForeignKey("Rol")]
        [Required]
        public int id_rol { get; set; }

        [Required(ErrorMessage = "Se requiere ingresar un nombre de usuario")]
        [StringLength(20, ErrorMessage = "Número de caracteres máximo excedido")]
        public string usuario { get; set; }

        [Required(ErrorMessage = "Se requiere ingresar una contraseña")]
        [MinLength(8, ErrorMessage = "La contraseña debe tener un mínimo de 8 caracteres")]
        public string pass { get; set; }

        [Required(ErrorMessage = "Se requiere ingresar nombre(s)")]
        [StringLength(50, ErrorMessage = "Número de caracteres máximo excedido")]
        public string nombres { get; set; }

        [Required(ErrorMessage = "Se requiere ingresar apellidos")]
        [StringLength(50, ErrorMessage = "Número de caracteres máximo excedido")]
        public string apellidos { get; set; }

        [Required(ErrorMessage = "Se requiere ingresar el tipo de documento")]
        [StringLength(20)]
        public string tipoDocumento { get; set; }

        [Required(ErrorMessage = "Se requiere ingresar el N° documento")]
        [MaxLength(8)]
        public string documento { get; set; }

        [Required(ErrorMessage = "Se requiere ingresar un correo válido")]
        [StringLength(50)]
        public string correo { get; set; }

        [Required(ErrorMessage = "Se requiere su número de celular")]
        [MaxLength(9)]
        public string celular { get; set; }

        public bool estado { get; set; }

        public Rol? Rol { get; set; }
        public ICollection<Reservas>? Add_Reserva { get; set; }
        public ICollection<User_Empresa>? Add_EmpresaUsuario { get; set; }
    }
}
