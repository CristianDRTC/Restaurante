using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gym.Models;

[Table("DetallePedido")]
public class DetallePedido
{
    [Key]
    public int IdDetallePedido { get; set; }

    public int IdPedido { get; set; }

    public int IdProducto { get; set; }

    public int Cantidad { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal PrecioUnitario { get; set; }

    [ForeignKey(nameof(IdPedido))]
    public virtual Pedido Pedido { get; set; } = null!;

    [ForeignKey(nameof(IdProducto))]
    public virtual Producto Producto { get; set; } = null!;
}