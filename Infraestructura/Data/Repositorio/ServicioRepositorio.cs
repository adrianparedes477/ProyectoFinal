using Core.Entidades;
using Infraestructura.Data.Repositorio.IRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Data.Repositorio
{
    public class ServicioRepositorio : Repositorio<Servicio>, IServicioRepositorio
    {
        private readonly ApplicationDbContext _db;

        public ServicioRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Actualizar(Servicio servicio)
        {
            var servicioDB = _db.Servicio.FirstOrDefault(s => s.Id == servicio.Id);

            if (servicioDB != null)
            {
                servicioDB.Descr = servicio.Descr;
                servicioDB.Estado = servicio.Estado;
                servicioDB.ValorHora = servicio.ValorHora;
                _db.SaveChanges();
            }
        }
    }
}
