using Core.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Data.Repositorio.IRepositorio
{
    public interface IUnidadTrabajo 
    {
        IProyectoRepositorio Proyecto { get; }
        IServicioRepositorio Servicio { get; }
        ITrabajoRepositorio Trabajo { get; }
        IUsuarioRepositorio Usuario { get; }
        Task Guardar();


    }
}
