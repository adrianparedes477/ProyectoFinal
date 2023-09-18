using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO
{
    public class UsuarioReedDTO
    {
        public string NombreCompleto { get; set; }
        public int Dni { get; set; }
        [Required]
        public string Contrasenia { get; set; }
        public string Tipo { get; set; }
    }
}
