using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO
{
    
        public class TrabajoDTO
        {
            public int Id { get; set; }
            public string Fecha { get; set; }
            public int CantHoras { get; set; }
            public decimal ValorHora { get; set; }
            public decimal Costo { get; set; }
        }

    
}
