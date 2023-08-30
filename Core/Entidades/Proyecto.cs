using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entidades
{
    public class Proyecto
    {
        public int CodProyecto { get; set; }

        public string Nombre { get; set; }

        public string Direccion { get; set; }

        public EstadoProyecto Estado { get; set; }

        public enum EstadoProyecto
        {
            Pendiente = 1,
            Confirmado = 2,
            Terminado = 3,
        }
    }
}
