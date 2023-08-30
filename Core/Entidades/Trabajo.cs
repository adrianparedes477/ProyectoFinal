using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entidades
{
    public class Trabajo
    {
        public int CodTrabajo { get; set; }

        public DateTime Fecha { get; set; }

        public int CodProyecto { get; set; }

        public int CodServicio { get; set; }

        public int CantHoras { get; set; }

        public decimal ValorHora { get; set; }

        public decimal Costo { get; set; }
    }
}
