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

    [ForeignKey("Deporte")]
    [Required(ErrorMessage = "Se requiere el deporte de la cancha")]
    public int id_deporte { get; set; }

    [Required(ErrorMessage = "Se requiere un nombre que identifque a la cancha")]
    [StringLength(30, ErrorMessage ="No puede exceder al número de 30 caracteres")]
    public string nombre { get; set; }

    [MaxLength(50)]
    public string? detalle { get; set; }

    [Required]
    [Column(TypeName = "time")]
    public TimeSpan horaApertura { get; set; }

    [Required]
    [Column(TypeName = "time")]
    public TimeSpan horaCierre { get; set; }

    [Required]
    public decimal costoPorHora { get; set; }

    [ForeignKey("Estado")]
    public int id_estadoCancha { get; set; }

    public Empresas? Empresa { get; set; }

    public Deportes? Deporte { get; set; }

    public Estado_Canchas? Estado { get; set; }

    public ICollection<Reservas>? Add_Reserva { get; set; }
}
}
