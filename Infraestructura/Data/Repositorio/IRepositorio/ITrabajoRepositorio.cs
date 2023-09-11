using Core.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Data.Repositorio.IRepositorio
{
    public interface ITrabajoRepositorio :IRepositorio<Trabajo>
    {
        void Actualizar(Trabajo trabajo);

        Task<Trabajo> GetByIdWithPropertiesAsync(int id);
    }
}
