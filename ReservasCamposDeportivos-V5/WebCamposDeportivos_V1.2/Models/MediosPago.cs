using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebCamposDeportivos_V1._2.Models
{
    public class MediosPago
    {
        [Key]
        public int id_pagos { get; set; }

        [ForeignKey("Empresas")]
        public int id_empresa { get; set; }

        [Required(ErrorMessage = "Obligatorio poner el tipo de pago")]
        [MaxLength(20)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Obligatorio escribir número de cuenta")]
        [MaxLength(15)]
        public string Descripcion { get; set; }

        public Empresas? Empresas { get; set; }
    }
}
