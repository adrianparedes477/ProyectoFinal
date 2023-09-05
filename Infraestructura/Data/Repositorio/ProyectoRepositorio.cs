using Core.Entidades;
using Infraestructura.Data.Repositorio.IRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Data.Repositorio
{
    internal class ProyectoRepositorio :Repositorio<Proyecto>,IProyectoRepositorio
    {
        private readonly ApplicationDbContext _db;

        public ProyectoRepositorio(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }

        public void Actualizar(Proyecto proyecto)
        {
            var proyectoDB = _db.Proyecto.FirstOrDefault(p => p.Id == proyecto.Id);

            if(proyectoDB != null)
            {
                proyectoDB.Nombre = proyecto.Nombre;
                proyectoDB.Direccion = proyecto.Direccion;
                proyectoDB.Estado = proyecto.Estado;
                _db.SaveChanges();
            }
        }
    }
}
