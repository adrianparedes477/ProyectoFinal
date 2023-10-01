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
    public class UsuarioConfiguracion : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasQueryFilter(x => !x.Borrado);

            builder.HasKey(x => x.Id);

            builder.Property(x=>x.NombreCompleto).IsRequired().HasMaxLength(60);
            builder.Property(x => x.Dni).IsRequired().HasMaxLength(60);
            builder.Property(x=>x.Contrasenia).IsRequired().HasMaxLength(64);
            builder.Property(x => x.Tipo).HasDefaultValue(Usuario.TipoUsuario.Consultor).IsRequired();
            builder.Property(x => x.Correo).IsRequired(false).HasMaxLength(64);
        }
    }
}
