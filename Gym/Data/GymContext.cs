using System;
using System.Collections.Generic;
using Gym.Models;
using Microsoft.EntityFrameworkCore;

namespace Gym.Data;

public partial class GymContext : DbContext
{
    public GymContext()
    {
    }

    public GymContext(DbContextOptions<GymContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Asistencia> Asistencia { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<Plan> Plans { get; set; }

    public virtual DbSet<Socio> Socios { get; set; }

    public virtual DbSet<Suscripcion> Suscripcions { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asistencia>(entity =>
        {
            entity.HasKey(e => e.IdAsistencia).HasName("PK__Asistenc__3956DEE6D5491A1D");

            entity.Property(e => e.FechaIngreso).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdSocioNavigation).WithMany(p => p.Asistencia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Asistenci__IdSoc__52593CB8");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.IdPago).HasName("PK__Pago__FC851A3AB57FEA8E");

            entity.Property(e => e.FechaPago).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdSuscripcionNavigation).WithMany(p => p.Pagos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pago__IdSuscripc__4BAC3F29");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Pagos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pago__IdUsuario__4F7CD00D");
        });

        modelBuilder.Entity<Plan>(entity =>
        {
            entity.HasKey(e => e.IdPlan).HasName("PK__Plan___FB8102AE84005439");

            entity.Property(e => e.Activo).HasDefaultValue(true);
        });

        modelBuilder.Entity<Socio>(entity =>
        {
            entity.HasKey(e => e.IdSocio).HasName("PK__Socio__863F0ECFF9E49913");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Suscripcion>(entity =>
        {
            entity.HasKey(e => e.IdSuscripcion).HasName("PK__Suscripc__B7CD0F53D254E0D0");

            entity.Property(e => e.Estado).HasDefaultValue("Activa");

            entity.HasOne(d => d.IdPlanNavigation).WithMany(p => p.Suscripcions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Suscripci__IdPla__46E78A0C");

            entity.HasOne(d => d.IdSocioNavigation).WithMany(p => p.Suscripcions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Suscripci__IdSoc__45F365D3");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF97D0DB743E");

            entity.Property(e => e.Activo).HasDefaultValue(true);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
