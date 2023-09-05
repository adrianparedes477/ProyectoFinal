using Core.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Data.Repositorio.IRepositorio
{
    public interface IServicioRepositorio :IRepositorio<Servicio>
    {
        void Actualizar(Servicio servicio);
    }
}
