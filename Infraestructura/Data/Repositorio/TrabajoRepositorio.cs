using Core.Entidades;
using Infraestructura.Data.Repositorio.IRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Data.Repositorio
{
    public class TrabajoRepositorio : Repositorio<Trabajo>, ITrabajoRepositorio
    {
        private readonly ApplicationDbContext _db;

        public TrabajoRepositorio(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }

        public void Actualizar(Trabajo trabajo)
        {
            var trabajoDB = _db.Trabajo.FirstOrDefault(t => t.Id == trabajo.Id); 

            if (trabajoDB != null)
            {
                trabajoDB.Fecha = trabajo.Fecha;
                trabajoDB.CantHoras = trabajo.CantHoras;
                trabajoDB.ValorHora = trabajo.ValorHora;
                trabajoDB.Costo = trabajo.Costo;
                _db.SaveChanges();
            }
        }
    }
}
