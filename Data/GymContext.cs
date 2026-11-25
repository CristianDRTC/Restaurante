using System;
using System.Collections.Generic;
using Gym.Models;
using Microsoft.EntityFrameworkCore;

namespace Gym.Data;

public class RestauranteContext : DbContext
{
    public RestauranteContext(DbContextOptions<RestauranteContext> options)
        : base(options)
    {
    }

    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Categoria> Categorias => Set<Categoria>();
    public DbSet<Producto> Productos => Set<Producto>();
    public DbSet<Mesa> Mesas => Set<Mesa>();
    public DbSet<Pedido> Pedidos => Set<Pedido>();
    public DbSet<DetallePedido> DetallesPedido => Set<DetallePedido>();
    public DbSet<Pago> Pagos => Set<Pago>();
    public DbSet<Usuario> Usuarios => Set<Usuario>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasIndex(e => e.Documento).IsUnique();
            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Activo).HasDefaultValue(true);
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasIndex(e => e.Nombre).IsUnique();
            entity.Property(e => e.Activo).HasDefaultValue(true);
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Disponible).HasDefaultValue(true);
            entity.HasOne(e => e.Categoria).WithMany(e => e.Productos)
                .HasForeignKey(e => e.IdCategoria).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Mesa>(entity =>
        {
            entity.HasIndex(e => e.Numero).IsUnique();
            entity.Property(e => e.Estado).HasDefaultValue("Disponible");
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.Property(e => e.FechaPedido).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Estado).HasDefaultValue("Pendiente");
            entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");
            entity.HasOne(e => e.Cliente).WithMany(e => e.Pedidos)
                .HasForeignKey(e => e.IdCliente).OnDelete(DeleteBehavior.SetNull);
            entity.HasOne(e => e.Mesa).WithMany(e => e.Pedidos)
                .HasForeignKey(e => e.IdMesa).OnDelete(DeleteBehavior.SetNull);
            entity.HasOne(e => e.Usuario).WithMany(e => e.Pedidos)
                .HasForeignKey(e => e.IdUsuario).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<DetallePedido>(entity =>
        {
            entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(10, 2)");
            entity.HasOne(e => e.Pedido).WithMany(e => e.Detalles)
                .HasForeignKey(e => e.IdPedido).OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.Producto).WithMany(e => e.DetallesPedido)
                .HasForeignKey(e => e.IdProducto).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.Property(e => e.Monto).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.FechaPago).HasDefaultValueSql("(getdate())");
            entity.HasOne(e => e.Pedido).WithMany(e => e.Pagos)
                .HasForeignKey(e => e.IdPedido).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasIndex(e => e.NombreUsuario).IsUnique();
            entity.Property(e => e.Activo).HasDefaultValue(true);
        });
    }
}
