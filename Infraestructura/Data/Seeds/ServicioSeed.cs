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
    public class ServicioSeeder : IEntityTypeConfiguration<Servicio>
    {
        public void Configure(EntityTypeBuilder<Servicio> builder)
        {
           builder.HasData(
               new Servicio
               {
                   Id = 1,
                   Descr = "Servicio 1",
                   Estado = true, // Activo
                   ValorHora = 10.50m
               },
               new Servicio
               {
                   Id = 2,
                   Descr = "Servicio 2",
                   Estado = false, // No Activo
                   ValorHora = 15.75m
               }
       
            );
        }
    }
}
