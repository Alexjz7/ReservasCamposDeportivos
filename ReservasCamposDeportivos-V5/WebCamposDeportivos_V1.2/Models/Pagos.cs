using System.ComponentModel.DataAnnotations;

namespace WebCamposDeportivos_V1._2.Models
{
    public class Pagos
    {
        [Key]
        public int id_pagos { get; set; }

        [Required(ErrorMessage ="Obligatorio poner el tipo de pago")]
        [MaxLength(30)]
        public string Descripcion { get; set; }

        public ICollection<Reservas>? Add_Reserva { get; set; }
    }
}
