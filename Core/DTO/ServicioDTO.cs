using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO
{
    public class ServicioDTO
    {
        public int CodServicio { get; set; }
        public string Descr { get; set; }
        public string Estado { get; set; } // Cambio de bool a string
        public decimal ValorHora { get; set; }
    }
}
