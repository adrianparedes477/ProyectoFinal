using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entidades
{
    public abstract class EntidadBase
    {
        public DateTime Creado { get; set; }
        public bool Borrado { get; set; }
        public DateTime UltimaModificacion { get; set; }
    }
}
