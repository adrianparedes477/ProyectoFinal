using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Modelos.DTO
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; }
        public int Dni { get; set; }
        public string Contrasenia { get; set; }
        public string Tipo { get; set; }
    }
}
