using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gym.Models;

[Table("Cliente")]
public class Cliente
{
    [Key]
    public int IdCliente { get; set; }

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

    public DateTime FechaRegistro { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}