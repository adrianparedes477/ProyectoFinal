﻿using Core.DTO;
using Core.Entidades;
using Infraestructura.Data.Repositorio.IRepositorio;
using Infraestructura.Data.Seeds;
using Microsoft.EntityFrameworkCore;
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

        public void Actualizar(Usuario usuario)
        {
            var usuarioDB = _db.Usuario.FirstOrDefault(u => u.Id == usuario.Id);

            if (usuarioDB != null)
            {
                usuarioDB.NombreCompleto = usuario.NombreCompleto;
                usuarioDB.Dni = usuario.Dni;
                usuarioDB.Tipo = usuario.Tipo;

                // Encripta la contraseña antes de guardarla
                usuarioDB.Contrasenia = PasswordEncryptHelper.EncryptPassword(usuario.Contrasenia);

                _db.SaveChanges();
            }
            else
            {
                throw new Exception("El Usuario no existe en la base de datos.");
            }
        }

        public async Task<Usuario?> AuthenticateCredentials(AuthenticateDto dto)
        {
            return await _db.Usuario
                .SingleOrDefaultAsync(x =>
                    x.NombreCompleto == dto.NombreCompleto &&
                    x.Dni == dto.Dni &&
                    x.Contrasenia == PasswordEncryptHelper.EncryptPassword(dto.Contrasenia)
                );
        }

    }
}
