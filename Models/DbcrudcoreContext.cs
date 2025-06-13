using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CrudCore.Models;

public partial class DbcrudcoreContext : DbContext
{
    public DbcrudcoreContext()
    {
    }

    public DbcrudcoreContext(DbContextOptions<DbcrudcoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cargo> Cargos { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    // ❌ Ya no se necesita OnConfiguring porque usamos inyección de dependencias en Program.cs

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cargo>(entity =>
        {
            entity.HasKey(e => e.IDcArgo).HasName("PK__CARGO__B09A730192DFB6C5");

            entity.ToTable("CARGO");

            entity.Property(e => e.IDcArgo).HasColumnName("iDcARGO");
            entity.Property(e => e.Descricion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.IdEmpleado).HasName("PK__EMPLEADO__CE6D8B9EF7B8434F");

            entity.ToTable("EMPLEADO");

            entity.Property(e => e.Correo)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(60)
                .IsUnicode(false);

            entity.HasOne(d => d.IdCargoNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdCargo)
                .HasConstraintName("FK__EMPLEADO__IdCarg__4BAC3F29");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
