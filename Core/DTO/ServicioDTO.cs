using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO
{
    public class ServicioDTO
    {
        public int Id { get; set; }
        public string Descr { get; set; }
        public bool Estado { get; set; } 
        public decimal ValorHora { get; set; }
    }
}
