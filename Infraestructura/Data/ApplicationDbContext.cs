using Core.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Proyecto> Proyecto { get; set; }
        public DbSet<Servicio> Servicio { get; set; }

        public DbSet<Trabajo> Trabajo { get; set; }

        public DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Servicio>(entity =>
            {
                entity.Property(e => e.ValorHora)
                    .HasColumnType("decimal(18, 2)");
                // O HasPrecision si prefieres configurar precisión y escala:
                // .HasPrecision(18, 2);
            });

            modelBuilder.Entity<Trabajo>(entity =>
            {
                entity.Property(e => e.Costo)
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ValorHora)
                    .HasColumnType("decimal(18, 2)");
            });

            
        }

    }


}
