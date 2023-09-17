using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO
{
    public class UsuarioReedDTO
    {
        public string NombreCompleto { get; set; }
        public int Dni { get; set; }
        public string Contrasenia { get; set; }
        public string Tipo { get; set; }
    }
}
