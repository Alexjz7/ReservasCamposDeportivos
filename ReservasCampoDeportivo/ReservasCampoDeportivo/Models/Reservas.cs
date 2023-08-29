using System.ComponentModel.DataAnnotations;

namespace ReservasCampoDeportivo.Models
{
    public class Reservas
    {
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
