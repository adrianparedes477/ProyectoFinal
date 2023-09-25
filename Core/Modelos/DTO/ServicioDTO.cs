using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Modelos.DTO
{
    public class ServicioReedDTO
    {
        [Required(ErrorMessage = "La Descr es requerido")]
        public string Descr { get; set; }
        [Required(ErrorMessage = "El Estado es requerido")]
        public bool Estado { get; set; }
        [Required(ErrorMessage = "El ValorHora es requerido")]
        public decimal ValorHora { get; set; }
    }
}
