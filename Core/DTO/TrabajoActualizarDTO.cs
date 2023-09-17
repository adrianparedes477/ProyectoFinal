using Core.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO
{

    public class TrabajoActualizarDTO
    {
        public int Id { get; set; }
        public string? Fecha { get; set; } // Usamos DateTime? para permitir valores nulos
        public int CantHoras { get; set; }
        public decimal ValorHora { get; set; }
        public decimal Costo { get; set; }

    }
}
