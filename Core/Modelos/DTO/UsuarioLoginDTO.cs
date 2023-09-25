using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Modelos.DTO
{
    public class UsuarioLoginDTO
    {
        public string NombreCompleto { get; set; }
        public int Dni { get; set; }
        public string Token { get; set; }

    }
}
