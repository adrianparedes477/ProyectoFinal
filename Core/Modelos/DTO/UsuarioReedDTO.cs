using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Modelos.DTO
{
    public class UsuarioReedDTO
    {
        public int Id { get; set; }

        [Display(Name = "Nombre del Usuario")]
        public string NombreCompleto { get; set; }
        public int Dni { get; set; }
        [Required]
        public string Contrasenia { get; set; }
        public string Tipo { get; set; }
        public string Correo { get; set; }
    }
}
