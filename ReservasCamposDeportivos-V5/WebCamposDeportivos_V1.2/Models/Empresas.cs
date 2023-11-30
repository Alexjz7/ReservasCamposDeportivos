using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebCamposDeportivos_V1._2.Models
{
    public class Empresas
    {
        [Key]
        public int id_empresa { get; set; }

        [Required(ErrorMessage = "Se requiere el nombre de la empresa")]
        [StringLength(30)]
        public string? nombre { get; set; }

        [Required]
        [StringLength(50)]
        public string? correo { get; set; }

        [Required]
        [StringLength(13)]
        public string? celular { get; set; }

        [Required]
        [Column(TypeName = "time")]
        public TimeSpan horaApertura { get; set; }

        [Required]
        [Column(TypeName = "time")]
        public TimeSpan horaCierre { get; set; }

        public ICollection<Canchas>? Add_Cancha { get; set; }

        public ICollection<User_Empresa>? Add_EmpresaUsuario { get; set; }

        public ICollection<MediosPago>? Add_MedioPago { get; set; }

        [NotMapped]
        public TimeSpan DiferenciaHoras
        {
            get
            {
                if (horaApertura > horaCierre)
                {
                    var horaNueva = horaCierre.Add(new TimeSpan(24, 0, 0));
                    return horaNueva - horaApertura;
                }

                return horaCierre - horaApertura;
            }
        }

        public IEnumerable<TimeSpan> ObtenerRangoHorasDisponibles()
        {
            return Enumerable.Range(horaApertura.Hours, DiferenciaHoras.Hours + 1)
                             .Select(h => TimeSpan.FromHours(h));
        }
    }
}
