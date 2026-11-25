using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Gym.Models;

[Table("Usuario")]
[Index("NombreUsuario", Name = "UQ__Usuario__6B0F5AE08BFAA463", IsUnique = true)]
public partial class Usuario
{
    [Key]
    public int IdUsuario { get; set; }

    [StringLength(50)]
    public string NombreUsuario { get; set; } = null!;

    [StringLength(255)]
    public string Clave { get; set; } = null!;

    [StringLength(20)]
    public string Rol { get; set; } = null!;

    public bool Activo { get; set; }

    [InverseProperty("IdUsuarioNavigation")]
    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
}
