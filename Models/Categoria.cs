using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gym.Models;

[Table("Categoria")]
public class Categoria
{
    [Key]
    public int IdCategoria { get; set; }

    [StringLength(60)]
    public string Nombre { get; set; } = null!;

    [StringLength(200)]
    public string? Descripcion { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}