using System.ComponentModel.DataAnnotations;

namespace Web_CamposDeportivos__V2.Models
{
    public class Reservas
    {
        [Key]
        public int id_reserva { get; set; }

        [Required]

        public int id_cancha { get; set; }
        
        [Required]

        public int id_cliente { get; set; }
        
        [Required]

        public DateTime fechaReserva { get; set; }

        public Canchas? Cancha { get; set; }

        public Usuarios? Cliente { get; set; }
    }
}
