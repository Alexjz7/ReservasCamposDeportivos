using System.ComponentModel.DataAnnotations;

namespace ReservasCampoDeportivo.Models
{
    public class Canchas
    {
        public int id_cancha { get; set; }

        [Required]
        public int id_gerente { get; set; }

        [Required]
        [StringLength(50)]
        public string? deporte { get; set; }

        [Required]
        public bool disponible { get; set; }

        [Required]
        public int cantidadCanchas { get; set; }

        [Required]
        public bool reservada { get; set; }

        public Gerentes? Gerente { get; set; } 
    }
}
