using System.ComponentModel.DataAnnotations;

namespace WebCamposDeportivos_V1._2.Models
{
    public class Deportes
    {
        [Key]
        public int id_deportes { get; set; }

        [Required(ErrorMessage ="Obligatorio el nombre del deporte")]
        [MaxLength(30)]
        public string nombre { get; set; }

        public ICollection<Canchas>? Add_Cancha { get; set; }
    }
}
