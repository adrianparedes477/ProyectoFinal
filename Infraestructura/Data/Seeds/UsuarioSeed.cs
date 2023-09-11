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
            PasswordManager passwordManager = new PasswordManager();

            builder.HasData(
               new Usuario
               {
                   Id = 1,
                   Nombre = "Admin",
                   Dni = 12345678,
                   Tipo = Usuario.TipoUsuario.Administrador,
                   Contrasenia = passwordManager.HashPassword("ContraseñaAdmin123")
               },
                new Usuario
                {
                    Id = 2,
                    Nombre = "Consultor",
                    Dni = 87654321,
                    Tipo = Usuario.TipoUsuario.Consultor,
                    Contrasenia = passwordManager.HashPassword("ContraseñaConsultor123")
                }
            );
        }
    }

}
