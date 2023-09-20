using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO
{
    public class ProyectoReedDto
    {
       
        [Required(ErrorMessage ="El Nombre es requerido")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "La Direccion es requerido")]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "El Estado es requerido")]
        public string Estado { get; set; } // Cambio de enum a string
    }
}
