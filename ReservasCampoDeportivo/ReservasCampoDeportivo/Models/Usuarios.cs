using System.ComponentModel.DataAnnotations;

namespace ReservasCampoDeportivo.Models
{
    public class Usuarios
    {
        public int id_usuario { get; set; }

        [Required]
        [StringLength(30)]
        public string? usuario { get; set; }

        [Required]
        [StringLength(50)]
        public string? pass { get; set; }

        [Required]
        [StringLength(50)]
        public string? Confirmarpass { get; set; }

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

        public bool validacion { get; set; }
    }
}
