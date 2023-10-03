using System.ComponentModel.DataAnnotations;

namespace WebCamposDeportivos_V1._2.Models
{
    public class Pago
    {
        [Key]
        public int id_pago { get; set; }

        [MaxLength(30)]
        public string? Descripcion { get; set; }

        public bool estado { get; set; }

        public ICollection<Reservas>? Add_Reserva { get; set; }
    }
}
