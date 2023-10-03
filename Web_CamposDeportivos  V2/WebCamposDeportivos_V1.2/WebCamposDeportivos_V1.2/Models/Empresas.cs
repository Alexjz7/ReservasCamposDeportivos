using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebCamposDeportivos_V1._2.Models
{
    public class Empresas
    {
        [Key]
        public int id_empresa { get; set; }

        [Required(ErrorMessage = "Se requiere el nombre de la empresa")]
        [StringLength(30)]
        public string? nombre { get; set; }

        [Required]
        [StringLength(50)]
        public string? correo { get; set; }

        [Required]
        [StringLength(13)]
        public string? celular { get; set; }

        public ICollection<Canchas>? Add_Cancha { get; set; }
        public ICollection<User_Empresa>? Add_EmpresaUsuario { get; set; }
    }
}
