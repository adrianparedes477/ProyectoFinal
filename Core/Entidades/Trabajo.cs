using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entidades
{
    public class Trabajo
    {
        [Column("codTrabajo")]
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="La fecha es requerida")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El CodProyecto es requerido")]
        public int CodProyecto { get; set; }

        [Required(ErrorMessage = "El CodServicio es requerido")]
        public int CodServicio { get; set; }

        [Required(ErrorMessage = "CantHoras es requerido")]
        [MaxLength(60,ErrorMessage = "CantHoras no puede tener mas de 60 caracteres")]
        public int CantHoras { get; set; }

        [Required(ErrorMessage = "ValorHora es requerido")]
        [MaxLength(60, ErrorMessage = "ValorHora no puede tener mas de 60 caracteres")]
        public decimal ValorHora { get; set; }

        [Required(ErrorMessage = "Costo es requerido")]
        [MaxLength(60, ErrorMessage = "Costo no puede tener mas de 60 caracteres")]
        public decimal Costo { get; set; }

        [ForeignKey("CodProyecto")]
        public Proyecto Proyecto { get; set; }

        [ForeignKey("CodServicio")]
        public Servicio Servicio { get; set; }

    }
}
