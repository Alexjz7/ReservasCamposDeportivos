using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebCamposDeportivos_V1._2.Models
{
    public class User_Empresa
    {
        public int User_EmpresaID { get; set; }

        [ForeignKey("Usuario")]
        [Required(ErrorMessage = "Se debe agregar un usuario")]
        public int id_usuario { get; set; }

        [ForeignKey("Empresa")]
        [Required(ErrorMessage = "No se olvide de la empresa")]
        public int id_empresa { get; set; }

        public Usuarios? Usuario { get; set; }
        public Empresas? Empresa { get; set; }
    }
}
