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
    public class ServicioConfiguracion : IEntityTypeConfiguration<Servicio>
    {
        public void Configure(EntityTypeBuilder<Servicio> builder)
        {
            builder.HasQueryFilter(x => !x.Borrado);

            builder.HasKey(x => x.Id);

            builder.Property(x=>x.Descr).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Estado).IsRequired().HasDefaultValue(false);
            builder.Property(x => x.ValorHora).IsRequired().HasMaxLength(60);
        }
    }
}
