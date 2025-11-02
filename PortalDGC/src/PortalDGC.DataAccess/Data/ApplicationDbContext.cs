using Microsoft.EntityFrameworkCore;
using PortalDGC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }
        public DbSet<Postulante> Postulantes { get; set; }
        public DbSet<Llamado> Llamados { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<LlamadoDepartamento> LlamadoDepartamentos { get; set; }
        public DbSet<Inscripcion> Inscripciones { get; set; }
        public DbSet<AutodefinicionLey> AutodefinicionesLey { get; set; }
        public DbSet<RequisitoExcluyente> RequisitosExcluyentes { get; set; }
        public DbSet<RequisitoPostulante> RequisitosPostulante { get; set; }
        public DbSet<ItemPuntuable> ItemsPuntuables { get; set; }
        public DbSet<MeritoPostulante> MeritosPostulante { get; set; }
        public DbSet<ApoyoNecesario> ApoyosNecesarios { get; set; }
        public DbSet<ApoyoSolicitado> ApoyosSolicitados { get; set; }
        public DbSet<Constancia> Constancias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Postulante>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Apellido).IsRequired().HasMaxLength(100);
                entity.Property(e => e.CedulaIdentidad).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(150);
                entity.Property(e => e.Celular).IsRequired().HasMaxLength(20);
                entity.HasIndex(e => e.CedulaIdentidad).IsUnique();
                entity.HasIndex(e => e.Email).IsUnique();
            });

            modelBuilder.Entity<Llamado>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Titulo).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Estado).IsRequired().HasMaxLength(50);
                entity.Property(e => e.PorcentajeAfrodescendiente).HasPrecision(5, 2);
                entity.Property(e => e.PorcentajeTrans).HasPrecision(5, 2);
                entity.Property(e => e.PorcentajeDiscapacidad).HasPrecision(5, 2);
            });

            modelBuilder.Entity<Departamento>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Codigo).IsRequired().HasMaxLength(10);
                entity.HasIndex(e => e.Codigo).IsUnique();
            });

            modelBuilder.Entity<LlamadoDepartamento>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Llamado)
                    .WithMany(l => l.LlamadoDepartamentos)
                    .HasForeignKey(e => e.LlamadoId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Departamento)
                    .WithMany(d => d.LlamadoDepartamentos)
                    .HasForeignKey(e => e.DepartamentoId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Inscripcion>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Estado).IsRequired().HasMaxLength(50);
                entity.Property(e => e.PuntajeTotal).HasPrecision(10, 2);

                entity.HasOne(e => e.Postulante)
                    .WithMany(p => p.Inscripciones)
                    .HasForeignKey(e => e.PostulanteId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Llamado)
                    .WithMany(l => l.Inscripciones)
                    .HasForeignKey(e => e.LlamadoId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Departamento)
                    .WithMany(d => d.Inscripciones)
                    .HasForeignKey(e => e.DepartamentoId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(e => new { e.PostulanteId, e.LlamadoId }).IsUnique();
            });

            modelBuilder.Entity<AutodefinicionLey>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Inscripcion)
                    .WithOne(i => i.AutodefinicionLey)
                    .HasForeignKey<AutodefinicionLey>(e => e.InscripcionId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<RequisitoExcluyente>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Descripcion).IsRequired().HasMaxLength(500);
                entity.Property(e => e.Tipo).IsRequired().HasMaxLength(100);

                entity.HasOne(e => e.Llamado)
                    .WithMany(l => l.RequisitosExcluyentes)
                    .HasForeignKey(e => e.LlamadoId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<RequisitoPostulante>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Inscripcion)
                    .WithMany(i => i.RequisitosPostulante)
                    .HasForeignKey(e => e.InscripcionId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Requisito)
                    .WithMany(r => r.RequisitosPostulante)
                    .HasForeignKey(e => e.RequisitoId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ItemPuntuable>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(200);
                entity.Property(e => e.PuntajeMaximo).HasPrecision(10, 2);
                entity.Property(e => e.Categoria).IsRequired().HasMaxLength(100);

                entity.HasOne(e => e.Llamado)
                    .WithMany(l => l.ItemsPuntuables)
                    .HasForeignKey(e => e.LlamadoId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<MeritoPostulante>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.PuntajeObtenido).HasPrecision(10, 2);

                entity.HasOne(e => e.Inscripcion)
                    .WithMany(i => i.MeritosPostulante)
                    .HasForeignKey(e => e.InscripcionId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.ItemPuntuable)
                    .WithMany(ip => ip.MeritosPostulante)
                    .HasForeignKey(e => e.ItemPuntuableId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ApoyoNecesario>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Descripcion).IsRequired().HasMaxLength(500);
                entity.Property(e => e.Tipo).IsRequired().HasMaxLength(100);

                entity.HasOne(e => e.Llamado)
                    .WithMany(l => l.ApoyosNecesarios)
                    .HasForeignKey(e => e.LlamadoId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ApoyoSolicitado>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Inscripcion)
                    .WithMany(i => i.ApoyosSolicitados)
                    .HasForeignKey(e => e.InscripcionId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Apoyo)
                    .WithMany(a => a.ApoyosSolicitados)
                    .HasForeignKey(e => e.ApoyoId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Constancia>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Tipo).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Archivo).IsRequired().HasMaxLength(500);

                entity.HasOne(e => e.Postulante)
                    .WithMany(p => p.Constancias)
                    .HasForeignKey(e => e.PostulanteId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
