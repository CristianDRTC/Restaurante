using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Gym.Models;

public partial class Asistencia
{
    [Key]
    public int IdAsistencia { get; set; }

    public int IdSocio { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime FechaIngreso { get; set; }

    [ForeignKey("IdSocio")]
    [InverseProperty("Asistencia")]
    public virtual Socio IdSocioNavigation { get; set; } = null!;
}
