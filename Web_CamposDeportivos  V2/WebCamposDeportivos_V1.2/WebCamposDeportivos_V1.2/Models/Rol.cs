using System.ComponentModel.DataAnnotations;

namespace WebCamposDeportivos_V1._2.Models
{
    public class Rol
    {
        public int RolID { get; set; }

        [MaxLength(30)] //Operario, Admin, Cliente
        public string Descripcion { get; set; }

        public ICollection<Usuarios>? Add_Usuario { get; set; }
    }
}
