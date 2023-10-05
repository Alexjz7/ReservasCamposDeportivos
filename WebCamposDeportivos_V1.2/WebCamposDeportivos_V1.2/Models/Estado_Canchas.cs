using System.ComponentModel.DataAnnotations;

namespace WebCamposDeportivos_V1._2.Models
{
    public class Estado_Canchas
    {
        [Key]
        public int Id_estadoCancha { get; set; }
        public string Descripcion { get; set; }

        public ICollection<Canchas>? Add_Cancha { get; set; }
    }
}
