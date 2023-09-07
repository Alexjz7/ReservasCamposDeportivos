﻿using System.ComponentModel.DataAnnotations;

namespace Web_CamposDeportivos__V2.Models
{
    public class Canchas
    {

        [Key]
        public int id_cancha { get; set; }

        [Required]
        public int id_gerente { get; set; }

        [Required(ErrorMessage = "Se requiere ingresar un campo")]
        [StringLength(50)]
        public string? deporte { get; set; }

        [Required]
        public bool disponible { get; set; }

        [Required]
        public bool reservada { get; set; }

        public Gerentes? Gerente { get; set; }

        public ICollection<Reservas>? Add_Reserva { get; set; }
    }
}
