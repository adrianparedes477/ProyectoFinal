using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entidades
{
    public class Trabajo
    {
        [Column("codTrabajo")]
        public int Id { get; set; }

        public DateTime Fecha { get; set; }

        public int CodProyecto { get; set; }

        public int CodServicio { get; set; }

        public int CantHoras { get; set; }

        public decimal ValorHora { get; set; }

        public decimal Costo { get; set; }

        [ForeignKey("CodProyecto")]
        public Proyecto Proyecto { get; set; }

        [ForeignKey("CodServicio")]
        public Servicio Servicio { get; set; }

    }
}
