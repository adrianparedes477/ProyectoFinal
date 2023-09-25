using Core.Entidades;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace Infraestructura.Helpers;

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
               Contrasenia = PasswordEncryptHelper.EncryptPassword("ContraseñaAdmin123"),
               Borrado = false,
               Creado = new DateTime(2023, 08, 30),
               UltimaModificacion = DateTime.Now


           },
            new Usuario
            {
                Id = 2,
                NombreCompleto = "Consultor",
                Dni = 87654321,
                Tipo = Usuario.TipoUsuario.Consultor,
                Contrasenia = PasswordEncryptHelper.EncryptPassword("ContraseñaConsultor123"),
                Borrado = false,
                Creado = new DateTime(2023, 05, 15)
            }
        );
    }
}
