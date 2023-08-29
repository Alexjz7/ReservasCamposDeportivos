using System.ComponentModel.DataAnnotations;

namespace ReservasCampoDeportivo.Models
{
    public class Gerentes
    {
        public int id_gerente { get; set; }

        [Required]
        [StringLength(30)]
        public string? usuario { get; set; }

        [Required]
        [StringLength(50)]
        public string? pass { get; set; }

        [Required]
        [StringLength(30)]
        public string? nombres { get; set; }

        [Required]
        [StringLength(30)]
        public string? apellidos { get; set; }

        [Required]
        [StringLength(15)]
        public string? tipoDocumento { get; set; }

        [Required]
        [StringLength(20)]
        public string? documento { get; set; }

        [Required]
        [StringLength(50)]
        public string? correo { get; set; }

        [Required]
        [StringLength(13)]
        public string? celular { get; set; }

        [Required]
        public bool validacion { get; set; }

        [Required]
        [StringLength(50)]
        public string? nombreCampo { get; set; }

        [Required]
        public TimeSpan horaApertura { get; set; }

        [Required]
        public TimeSpan horaCierre { get; set; }

        [Required]
        public decimal costoPorHora { get; set; }
    }
}
