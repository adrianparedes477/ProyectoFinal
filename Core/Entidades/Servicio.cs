using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entidades
{
    public class Servicio
    {
        public int CodServicio { get; set; }

        public string Descr { get; set; }

        public bool Estado { get; set; }

        public decimal ValorHora { get; set; }
    }
}
