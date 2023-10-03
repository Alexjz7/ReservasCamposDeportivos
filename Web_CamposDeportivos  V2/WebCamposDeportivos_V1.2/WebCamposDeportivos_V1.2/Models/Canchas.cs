﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebCamposDeportivos_V1._2.Models
{
    public class Canchas
{

    [Key]
    public int id_cancha { get; set; }

    [ForeignKey("Empresa")]
    [Required(ErrorMessage = "Se requiere el deporte de la cancha")]
    public int id_empresa { get; set; }

    [ForeignKey("Deporte")]
    [Required(ErrorMessage = "Se requiere el deporte de la cancha")]
    public int id_deporte { get; set; }

    [Required(ErrorMessage = "Se requiere un nombre para la cancha")]
    [StringLength(50)]
    public string? detalle { get; set; }

    [Required]
    [Column(TypeName = "date")]
    public DateTime fechaInicio { get; set; }

    [Required]
    [Column(TypeName = "date")]
    public DateTime fechaFinal { get; set; }

    [Required]
    [Column(TypeName = "time")]
    public TimeSpan horaApertura { get; set; }

    [Required]
    [Column(TypeName = "time")]
    public TimeSpan horaCierre { get; set; }

    [Required]
    public double costoPorHora { get; set; }

    public bool estado { get; set; }

    public Empresas? Empresa { get; set; }

    public Deportes? Deporte { get; set; }

    public ICollection<Reservas>? Add_Reserva { get; set; }
}
}