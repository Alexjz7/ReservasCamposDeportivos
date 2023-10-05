using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebCamposDeportivos_V1._2.Models
{
    public class Reservas
    {
        [Key]
        public int id_reserva { get; set; }

        [Required]
        public int ClienteID { get; set; }

        [Required]
        public int CanchaID { get; set; }

        [Required]
        public int PagoID { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime fechaReserva { get; set; }

        [Required]
        [Column(TypeName = "time")]
        public TimeSpan horaReserva { get; set; }

        [Required]
        public decimal total { get; set; }

        public string? estado { get; set; }

        public Usuarios? Cliente { get; set; }

        public Canchas? Cancha { get; set; }

        public Pagos? Pago { get; set; }
    }
}
