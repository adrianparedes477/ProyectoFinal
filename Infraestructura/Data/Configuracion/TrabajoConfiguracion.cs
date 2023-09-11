using Core.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Infraestructura.Data.Configuracion
{
    public class TrabajoConfiguracion : IEntityTypeConfiguration<Trabajo>
    {
        public void Configure(EntityTypeBuilder<Trabajo> builder)
        {
            

            builder.HasKey(t => t.Id); 

            builder.Property(t => t.Fecha).IsRequired();
            builder.Property(t => t.CodProyecto).IsRequired(); 
            builder.Property(t => t.CodServicio).IsRequired(); 
            builder.Property(t => t.CantHoras).IsRequired().HasMaxLength(60); 
            builder.Property(t => t.ValorHora).IsRequired().HasMaxLength(60); 
            builder.Property(t => t.Costo).IsRequired().HasMaxLength(60); 

            builder.HasOne(t => t.Proyecto) // Relación con Proyecto
                .WithMany()
                .HasForeignKey(t => t.CodProyecto);

            builder.HasOne(t => t.Servicio) // Relación con Servicio
                .WithMany()
                .HasForeignKey(t => t.CodServicio);
        }
    }
}
