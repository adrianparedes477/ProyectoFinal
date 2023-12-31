﻿using API.Especificaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Data.Repositorio.IRepositorio
{
    public interface IRepositorio<T> where T : class
    {
        Task<T> GetById(int id);

        Task<IEnumerable<T>> GetAll(
             Expression<Func<T, bool>> filtro = null,
             Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
             string incluirPropiedades = null,
             bool isTracking = true
             );

        Task<PagedList<T>> ObtenerTodosPaginado(
           Parametros parametros,
           Expression<Func<T, bool>> filtro = null,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
           string incluirPropiedades = null  
        );

        Task<bool> Existe(Expression<Func<T, bool>> filtro);

        Task<T> ObtenerPrimero(
            Expression<Func<T, bool>> filtro = null,
            string incluirPropiedades = null,
            bool isTracking = true
            );

        Task Agregar(T entidad);


        //void Remover(T entidad);

        Task<bool> Eliminar(int id);

        void RemoverRango(IEnumerable<T> entidad);

    }
}
