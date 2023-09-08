using Core.Entidades;
using Infraestructura.Data.Repositorio.IRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Data.Repositorio
{
    public class UnidadTrabajo : IUnidadTrabajo
    {
        private readonly ApplicationDbContext _db;
        public IProyectoRepositorio Proyecto { get; private set; }
        public IServicioRepositorio Servicio { get; private set; }
        public ITrabajoRepositorio Trabajo { get; private set; }
        public IUsuarioRepositorio Usuario { get; private set; }

        public UnidadTrabajo(ApplicationDbContext db)
        {
            _db = db;
            Proyecto = new ProyectoRepositorio(_db);
            Servicio = new ServicioRepositorio(_db);
            Trabajo = new TrabajoRepositorio(_db);
            Usuario = new UsuarioRepositorio(_db);

        }
        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task Guardar()
        {
            await _db.SaveChangesAsync();
        }
    }


}
