using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Gym.Models;

[Table("Socio")]
[Index("Documento", Name = "UQ__Socio__AF73706D2CA831E8", IsUnique = true)]
public partial class Socio
{
    [Key]
    public int IdSocio { get; set; }

    [StringLength(20)]
    public string Documento { get; set; } = null!;

    [StringLength(80)]
    public string Nombres { get; set; } = null!;

    [StringLength(80)]
    public string Apellidos { get; set; } = null!;

    [StringLength(20)]
    public string? Telefono { get; set; }

    [StringLength(100)]
    public string? Correo { get; set; }

    public DateOnly? FechaNacimiento { get; set; }

    public DateOnly FechaRegistro { get; set; }

    public bool Activo { get; set; }

    [InverseProperty("IdSocioNavigation")]
    public virtual ICollection<Asistencia> Asistencia { get; set; } = new List<Asistencia>();

    [InverseProperty("IdSocioNavigation")]
    public virtual ICollection<Suscripcion> Suscripcions { get; set; } = new List<Suscripcion>();
}
