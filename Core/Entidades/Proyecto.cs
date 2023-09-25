using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entidades
{
    public class Proyecto : EntidadBase
    {
        [Column("codProyecto")]
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="El nombre es Requerido")]
        [MaxLength(60,ErrorMessage ="El nombre debe tener un maximo de 60 caracteres")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El Dirrecion es Requerido")]
        [MaxLength(60, ErrorMessage = "El Dirrecion debe tener un maximo de 60 caracteres")]
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
