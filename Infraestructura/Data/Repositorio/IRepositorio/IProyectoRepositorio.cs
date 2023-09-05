using Core.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Data.Repositorio.IRepositorio
{
    public interface IProyectoRepositorio :IRepositorio<Proyecto>
    {
        void Actualizar(Proyecto proyecto);
    }
}
