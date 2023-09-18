using Core.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Data.Seeds
{
    public class ProyectoSeeder : IEntityTypeConfiguration<Proyecto>
    {
        public void Configure(EntityTypeBuilder<Proyecto> builder)
        {
            builder.HasData(
                new Proyecto
                {
                    Id = 1,
                    Nombre = "Proyecto A",
                    Direccion = "Calle falsa 123",
                    Estado = Proyecto.EstadoProyecto.Confirmado,
                    Borrado = false,
                    Creado = new DateTime(2023, 02, 03),
                    UltimaModificacion = DateTime.Now
                },
                new Proyecto
                {
                    Id = 2,
                    Nombre = "Proyecto B",
                    Direccion = "Calle falsa 456",
                    Estado = Proyecto.EstadoProyecto.Terminado,
                    Borrado = false,
                    Creado = new DateTime(2023, 02, 02),
                    UltimaModificacion = DateTime.Now
                },
                new Proyecto
                {
                    Id = 3,
                    Nombre = "Proyecto C",
                    Direccion = "Calle falsa 789",
                    Estado = Proyecto.EstadoProyecto.Pendiente,
                    Borrado = false,
                    Creado = new DateTime(2023, 01, 09),
                    UltimaModificacion = DateTime.Now
                }
            );
                
        }
    }
}
