using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Gym.Models;

[Table("Suscripcion")]
public partial class Suscripcion
{
    [Key]
    public int IdSuscripcion { get; set; }

    public int IdSocio { get; set; }

    public int IdPlan { get; set; }

    public DateOnly FechaInicio { get; set; }

    public DateOnly FechaFin { get; set; }

    [StringLength(15)]
    public string Estado { get; set; } = null!;

    [ForeignKey("IdPlan")]
    [InverseProperty("Suscripcions")]
    public virtual Plan IdPlanNavigation { get; set; } = null!;

    [ForeignKey("IdSocio")]
    [InverseProperty("Suscripcions")]
    public virtual Socio IdSocioNavigation { get; set; } = null!;

    [InverseProperty("IdSuscripcionNavigation")]
    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
}
