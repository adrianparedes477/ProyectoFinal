using Core.Entidades;
using Infraestructura.Data.Configuracion;
using Infraestructura.Data.Seeds;
using Infraestructura.Helpers;
using Microsoft.EntityFrameworkCore;

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

            modelBuilder.ApplyConfiguration(new TrabajoConfiguracion());
            modelBuilder.ApplyConfiguration(new UsuarioConfiguracion());
            modelBuilder.ApplyConfiguration(new ServicioConfiguracion());
            modelBuilder.ApplyConfiguration(new ProyectoConfiguracion());

            modelBuilder.ApplyConfiguration(new UsuarioSeeder());
            modelBuilder.ApplyConfiguration(new ProyectoSeeder());
            modelBuilder.ApplyConfiguration(new ServicioSeeder());
            modelBuilder.ApplyConfiguration(new TrabajoSeeder());


            modelBuilder.Entity<Servicio>(entity =>
            {
                entity.Property(e => e.ValorHora)
                    .HasColumnType("decimal(18, 2)");
                
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
