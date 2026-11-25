using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gym.Models;

[Table("Producto")]
public class Producto
{
    [Key]
    public int IdProducto { get; set; }

    public int IdCategoria { get; set; }

    [StringLength(100)]
    public string Nombre { get; set; } = null!;

    [StringLength(300)]
    public string? Descripcion { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal Precio { get; set; }

    public bool Disponible { get; set; }

    [ForeignKey(nameof(IdCategoria))]
    public virtual Categoria Categoria { get; set; } = null!;

    public virtual ICollection<DetallePedido> DetallesPedido { get; set; } = new List<DetallePedido>();
}