using System.ComponentModel.DataAnnotations;

namespace WebCamposDeportivos_V1._2.Models
{
    public class Deportes
    {
        [Key]
        public int id_deporte {  get; set; }

        [MaxLength(30)]
        public string? Descripcion { get; set; }

        public bool estado { get; set; }

        public ICollection<Canchas>? Add_Cancha { get; set; }
    }
}
