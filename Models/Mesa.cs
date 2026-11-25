using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gym.Models;

[Table("Mesa")]
public class Mesa
{
    [Key]
    public int IdMesa { get; set; }

    public int Numero { get; set; }

    public int Capacidad { get; set; }

    [StringLength(20)]
    public string Estado { get; set; } = "Disponible";

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}