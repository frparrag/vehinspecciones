using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Inspecciones.Models
{
    public partial class DB_INSPECCIONESContext : DbContext
    {
        public DB_INSPECCIONESContext()
        {
        }

        public DB_INSPECCIONESContext(DbContextOptions<DB_INSPECCIONESContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Modulo> Modulo { get; set; }
        public virtual DbSet<Operaciones> Operaciones { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<RolOperacion> RolOperacion { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Host=localhost;Database=DB_INSPECCIONES;Username=postgres;Password=17357997");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Modulo>(entity =>
            {
                entity.ToTable("modulo", "usuarios");

                entity.HasComment("modulos del sistema");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nombre)
                    .HasColumnName("nombre")
                    .HasMaxLength(200)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Operaciones>(entity =>
            {
                entity.ToTable("operaciones", "usuarios");

                entity.HasComment("Operaciones que tiene cada modulo");

                entity.HasIndex(e => e.IdModulo)
                    .HasName("fki_fk_modulos_operaciones");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdModulo).HasColumnName("idModulo");

                entity.Property(e => e.Nombre)
                    .HasColumnName("nombre")
                    .HasMaxLength(200)
                    .IsFixedLength();

                entity.HasOne(d => d.IdModuloNavigation)
                    .WithMany(p => p.Operaciones)
                    .HasForeignKey(d => d.IdModulo)
                    .HasConstraintName("fk_modulos_operaciones");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.ToTable("rol", "usuarios");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nombre)
                    .HasColumnName("nombre")
                    .HasMaxLength(100)
                    .IsFixedLength();
            });

            modelBuilder.Entity<RolOperacion>(entity =>
            {
                entity.ToTable("rol_operacion", "usuarios");

                entity.HasComment("tabla para indicar que usuario y rol  que pueden hacer  sobre que modulo");

                entity.HasIndex(e => e.IdOperaciones)
                    .HasName("fki_fk_rol_operaciones_operaciones");

                entity.HasIndex(e => e.IdRol)
                    .HasName("fki_fk_rol_operacion");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdOperaciones).HasColumnName("idOperaciones");

                entity.Property(e => e.IdRol).HasColumnName("idRol");

                entity.HasOne(d => d.IdOperacionesNavigation)
                    .WithMany(p => p.RolOperacion)
                    .HasForeignKey(d => d.IdOperaciones)
                    .HasConstraintName("fk_rol_operaciones_operaciones");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.RolOperacion)
                    .HasForeignKey(d => d.IdRol)
                    .HasConstraintName("fk_rol_operacion");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("usuario", "usuarios");

                entity.HasIndex(e => e.IdRol)
                    .HasName("fki_FK_USUARIOS_ROL");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(200)
                    .IsFixedLength();

                entity.Property(e => e.Fecha)
                    .HasColumnName("fecha")
                    .HasColumnType("date");

                entity.Property(e => e.IdRol).HasColumnName("idRol");

                entity.Property(e => e.Nombre)
                    .HasColumnName("nombre")
                    .HasMaxLength(200)
                    .IsFixedLength();

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.IdRol)
                    .HasConstraintName("FK_USUARIOS_ROL");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
