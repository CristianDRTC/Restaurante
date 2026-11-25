using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gym.Models;

[Table("Pago")]
public class Pago
{
    [Key]
    public int IdPago { get; set; }

    public int IdPedido { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal Monto { get; set; }

    public DateTime FechaPago { get; set; }

    [StringLength(20)]
    public string MetodoPago { get; set; } = null!;

    [ForeignKey(nameof(IdPedido))]
    public virtual Pedido Pedido { get; set; } = null!;
}