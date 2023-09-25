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
    public class TrabajoSeeder : IEntityTypeConfiguration<Trabajo>
    {
        public void Configure(EntityTypeBuilder<Trabajo> builder)
        {
            builder.HasData(
                new Trabajo
                {
                    Id = 1,
                    Fecha = new DateTime(2023, 08, 30),
                    CodProyecto = 1,
                    CodServicio = 1,
                    CantHoras = 20,
                    ValorHora = 10,
                    Costo = 20,
                    Borrado = false,
                    Creado = new DateTime(2023, 08, 30),
                    UltimaModificacion = DateTime.Now


                }
            );
        }
    }
}
