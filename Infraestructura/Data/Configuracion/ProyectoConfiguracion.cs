using Core.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Data.Configuracion
{
    public class ProyectoConfiguracion : IEntityTypeConfiguration<Proyecto>
    {
        public void Configure(EntityTypeBuilder<Proyecto> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x=>x.Nombre).IsRequired().HasMaxLength(60);
            builder.Property(x => x.Direccion).IsRequired().HasMaxLength(60);
            builder.Property(x => x.Estado).HasDefaultValue(Proyecto.EstadoProyecto.Pendiente);
        }
    }
    
}
