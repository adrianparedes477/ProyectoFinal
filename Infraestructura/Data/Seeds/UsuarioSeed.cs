using Core.Entidades;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Helpers;

namespace Infraestructura.Data.Seeds
{
    public class UsuarioSeeder : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            

            builder.HasData(
               new Usuario
               {
                   Id = 1,
                   NombreCompleto = "Admin",
                   Dni = 12345678,
                   Tipo = Usuario.TipoUsuario.Administrador,
                   Contrasenia = PasswordEncryptHelper.EncryptPassword("ContraseñaAdmin123")
               },
                new Usuario
                {
                    Id = 2,
                    NombreCompleto = "Consultor",
                    Dni = 87654321,
                    Tipo = Usuario.TipoUsuario.Consultor,
                    Contrasenia = PasswordEncryptHelper.EncryptPassword("ContraseñaConsultor123")
                }
            );
        }
    }

}
