﻿using API.Helpers;
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
                    Fecha = DateTime.Now,
                    CodProyecto = 1,
                    CodServicio = 1,
                    CantHoras = 10,
                    ValorHora = 5,
                    Costo = 10

                }
            );
        }
    }
}
