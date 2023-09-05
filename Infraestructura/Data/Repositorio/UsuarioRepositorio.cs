using Core.Entidades;
using Infraestructura.Data.Repositorio.IRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Data.Repositorio
{
    public class UsuarioRepositorio :Repositorio<Usuario>,IUsuarioRepositorio
    {
        private readonly ApplicationDbContext _db;

        public UsuarioRepositorio(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }

        public void Actulizar(Usuario usuario)
        {
            var usuarioDB = _db.Usuario.FirstOrDefault(u=>u.Id == usuario.Id);

            if (usuarioDB != null) 
            {
                usuarioDB.Nombre = usuario.Nombre;
                usuarioDB.Dni = usuario.Dni;
                usuarioDB.Tipo = usuario.Tipo;
                usuario.Contrasenia = usuario.Contrasenia;
                _db.SaveChanges();
            }
        }
    }
}
