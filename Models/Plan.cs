using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Gym.Models;

[Table("Plan_")]
public partial class Plan
{
    [Key]
    public int IdPlan { get; set; }

    [StringLength(50)]
    public string Nombre { get; set; } = null!;

    public int DuracionDias { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal Precio { get; set; }

    public bool Activo { get; set; }

    [InverseProperty("IdPlanNavigation")]
    public virtual ICollection<Suscripcion> Suscripcions { get; set; } = new List<Suscripcion>();
}
