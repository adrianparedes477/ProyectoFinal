using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entidades
{
    public class Usuario :EntidadBase
    {
        [Column("codUsuario")]
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="El nombre es Requerido")]
        [MaxLength(60,ErrorMessage ="El nombre no puede tener mas de 60 caracteres")]
        public string NombreCompleto { get; set; }

        [Required(ErrorMessage = "El Dni es Requerido")]
        [MaxLength(60, ErrorMessage = "El dni no puede tener mas de 60 caracteres")]
        public int  Dni { get; set; }

        public TipoUsuario Tipo { get; set; }

        [Required(ErrorMessage = "La contraseña  es Requerida")]
        [MaxLength(64, ErrorMessage = "El contraseña no puede tener mas de 64 caracteres")]
        public string Contrasenia { get; set; }

        public enum TipoUsuario
        {
            Administrador = 1,
            Consultor = 2
        }
    }
}
