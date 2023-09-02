using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO
{
    
        public class TrabajoDTO
        {
            public int CodTrabajo { get; set; }
            public string Fecha { get; set; } // cambio de Datetime a string
            public string NombreProyecto { get; set; } // Solo el nombre del proyecto
            public string NombreServicio { get; set; } // Solo el nombre del servicio
            public int CantHoras { get; set; }
            public decimal ValorHora { get; set; }
            public decimal Costo { get; set; }
        }

    
}
