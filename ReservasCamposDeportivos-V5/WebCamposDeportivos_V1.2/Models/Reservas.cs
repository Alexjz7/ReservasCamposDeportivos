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

        [ForeignKey("Campo")]
        [Required(ErrorMessage = "Campo requerido")]
        public int CampoID { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime fechaReserva { get; set; }

        [Required]
        [Column(TypeName = "time")]
        public TimeSpan horaReserva { get; set; }

        [Required(ErrorMessage ="Debes llenar el número de horas")]
        [Range(1, 10, ErrorMessage = "El número de horas debe estar entre {1} y {2}")]
        public int numeroHoras { get; set; }

        [Required]
        public decimal total { get; set; }

        public string? estado { get; set; }

        [Column(TypeName = "time")]
        [DisplayFormat(DataFormatString = @"hh\:mm\:ss", ApplyFormatInEditMode = true)]
        public TimeSpan? TiempoConfirmacion { get; set; }

        [Column(TypeName = "date")]
        public DateTime FechaCreacion { get; set; }

        public Canchas? Campo { get; set; }

        public Usuarios? Cliente { get; set; }

        public decimal ObtenerCostoHora(TimeSpan horaCampo, Canchas canchas)
        {
            decimal costoDia = canchas.costoDia;
            decimal costoNoche = canchas.costoNoche;

            // Determinamos si la hora seleccionada se encuentra durante el día o la noche
            var tiempo = horaCampo.Hours >= 6 && horaCampo.Hours < 18;

            //Seleccionamos el costo correspondiente
            return tiempo ? costoDia : costoNoche;
        }
    }
}