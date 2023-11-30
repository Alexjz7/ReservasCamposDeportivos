using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebCamposDeportivos_V1._2.Models
{
    public class Canchas
    {
        [Key]
        public int id_cancha { get; set; }

        [ForeignKey("Empresa")]
        [Required(ErrorMessage = "Se requiere la empresa de la cancha")]
        public int id_empresa { get; set; }

        [MaxLength(10)]
        [Required(ErrorMessage = "Se requiere el deporte de la cancha")]
        public string? deporte { get; set; }

        [MaxLength(50)]
        public string? detalle { get; set; }

        [Required(ErrorMessage = "Costo del día requerido")]
        [Range(0, double.MaxValue, ErrorMessage = "El Precio no puede ser negativo")]
        public decimal costoDia { get; set; }

        [Required(ErrorMessage = "Costo de la noche requerido")]
        [Range(0, double.MaxValue, ErrorMessage = "El Precio no puede ser negativo")]
        public decimal costoNoche { get; set; }

        public string? UrlImage { get; set; }

        public Empresas? Empresa { get; set; }

        public ICollection<Reservas>? Add_Reserva { get; set; }
    }
}
