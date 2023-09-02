using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.Entidades.Usuario;

namespace Core.DTO
{
    public class LoginAdminDTO
    {
        public string Nombre { get; set; }

        public string Contrasenia { get; set; }

        public TipoUsuario Tipo { get; set; }

    }
}
