using API.Especificaciones;
using Core.Entidades;
using Infraestructura.Data.Repositorio.IRepositorio;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace Infraestructura.Data.Repositorio
{
    public class Repositorio<T> : IRepositorio<T> where T : EntidadBase
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        public Repositorio(ApplicationDbContext db)
        {
            _db = db;
            dbSet = _db.Set<T>();
        }

        public async Task Agregar(T entidad)
        {
            await dbSet.AddAsync(entidad); // insert into Table
        }

        public async Task<bool> Eliminar(int id)
        {
            var entidad = await dbSet.FindAsync(id);
            if (entidad == null)
                return false;

            entidad.Borrado = true;
            entidad.UltimaModificacion = DateTime.Now;

            return true;
        }



        public async Task<bool> Existe(Expression<Func<T, bool>> filtro)
        {
            return await dbSet.AnyAsync(filtro);
        }

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filtro = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string incluirPropiedades = null, bool isTracking = true)
        {
            IQueryable<T> query = dbSet;
            if (filtro != null)
            {
                query = query.Where(filtro); // select * from where.....
            }
            if (incluirPropiedades != null)
            {
                foreach (var incluirProp in incluirPropiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(incluirProp);
                }
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (!isTracking)
            {
                query = query.AsNoTracking();
            }
            return await query.ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await dbSet.FindAsync(id); // select * from (solo por Id)
        }

        public async Task<T> ObtenerPrimero(Expression<Func<T, bool>> filtro = null, string incluirPropiedades = null, bool isTracking = true)
        {
            IQueryable<T> query = dbSet;
            if (filtro != null)
            {
                query = query.Where(filtro); // select * from where.....
            }
            if (incluirPropiedades != null)
            {
                foreach (var incluirProp in incluirPropiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(incluirProp);
                }
            }
            if (!isTracking)
            {
                query = query.AsNoTracking();
            }
            return await query.FirstOrDefaultAsync();
        }



        public async Task<PagedList<T>> ObtenerTodosPaginado(Parametros parametros,
         Expression<Func<T, bool>> filtro = null, Func<IQueryable<T>,
         IOrderedQueryable<T>> orderBy = null, string incluirPropiedades = null)
        {
            IQueryable<T> query = dbSet;
            if (filtro != null)
            {
                query = query.Where(filtro);
            }
            if (incluirPropiedades != null)   
            {
                foreach (var ip in incluirPropiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(ip);
                }
            }
            if (orderBy != null)
            {
                await orderBy(query).ToListAsync();
                return PagedList<T>.ToPagedList(query, parametros.PageNumber, parametros.PageSize);
            }

            return PagedList<T>.ToPagedList(query, parametros.PageNumber, parametros.PageSize);
        }

        //public void Remover(T entidad)
        //{
        //    dbSet.Remove(entidad);
        //}

        public void RemoverRango(IEnumerable<T> entidad)
        {
            dbSet.RemoveRange(entidad);
        }
    }
}
