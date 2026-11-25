using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Gym.Models;

[Table("Pago")]
public partial class Pago
{
    [Key]
    public int IdPago { get; set; }

    public int IdSuscripcion { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal Monto { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime FechaPago { get; set; }

    [StringLength(20)]
    public string MetodoPago { get; set; } = null!;

    public int IdUsuario { get; set; }

    [ForeignKey("IdSuscripcion")]
    [InverseProperty("Pagos")]
    public virtual Suscripcion IdSuscripcionNavigation { get; set; } = null!;

    [ForeignKey("IdUsuario")]
    [InverseProperty("Pagos")]
    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
