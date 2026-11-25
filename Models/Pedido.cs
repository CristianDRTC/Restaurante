using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gym.Models;

[Table("Pedido")]
public class Pedido
{
    [Key]
    public int IdPedido { get; set; }

    public int? IdCliente { get; set; }

    public int? IdMesa { get; set; }

    public int IdUsuario { get; set; }

    public DateTime FechaPedido { get; set; }

    [StringLength(20)]
    public string Estado { get; set; } = "Pendiente";

    [Column(TypeName = "decimal(10, 2)")]
    public decimal Total { get; set; }

    [ForeignKey(nameof(IdCliente))]
    public virtual Cliente? Cliente { get; set; }

    [ForeignKey(nameof(IdMesa))]
    public virtual Mesa? Mesa { get; set; }

    [ForeignKey(nameof(IdUsuario))]
    public virtual Usuario Usuario { get; set; } = null!;

    public virtual ICollection<DetallePedido> Detalles { get; set; } = new List<DetallePedido>();

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
}