using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Gym.Models;

[Table("Usuario")]
[Index("NombreUsuario", Name = "UX_Usuario_NombreUsuario", IsUnique = true)]
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

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
