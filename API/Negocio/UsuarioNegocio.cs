﻿using AutoMapper;
using Core.DTO;
using Core.Entidades;
using Core.Negocio.INegocio;
using Infraestructura.Data.Repositorio;
using Infraestructura.Data.Repositorio.IRepositorio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Negocio
{
    public class UsuarioNegocio : IUsuarioNegocio
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly IMapper _mapper;
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioNegocio(IUnidadTrabajo unidadTrabajo, IMapper mapper, IUsuarioRepositorio usuarioRepositorio)
        {
            _unidadTrabajo = unidadTrabajo;
            _mapper = mapper;
            _usuarioRepositorio = usuarioRepositorio;
        }

        public async Task<IEnumerable<UsuarioDTO>> GetAllUsuarios()
        {
            var usuarios = await _unidadTrabajo.Usuario.GetAll();
            return _mapper.Map<IEnumerable<UsuarioDTO>>(usuarios);
        }

        public async Task<UsuarioDTO> GetUsuarioById(int id)
        {
            var usuario = await _unidadTrabajo.Usuario.GetById(id);
            return _mapper.Map<UsuarioDTO>(usuario);
        }

        public async Task<bool> CrearUsuario(UsuarioDTO usuarioDTO)
        {
            var usuario = _mapper.Map<Usuario>(usuarioDTO);
            // Verificar si el usuario existe
            var usuarioExiste = await _usuarioRepositorio.ObtenerPrimero(t => t.Id == usuario.Id);
            if (usuarioExiste != null)
            {
                await _unidadTrabajo.Usuario.Agregar(usuario);
                await _unidadTrabajo.Guardar();
                return true;
            }
            else
            {
                throw new Exception("El trabajo no existe en la base de datos.");
            }


        }

        public async Task<bool> ActualizarUsuario(UsuarioDTO usuarioDTO)
        {
            var usuario = _mapper.Map<Usuario>(usuarioDTO);
            _usuarioRepositorio.Actualizar(usuario); // Utiliza el método de repositorio
            await _unidadTrabajo.Guardar();
            return true;
        }

        public async Task<bool> EliminarUsuario(int id)
        {
            var usuario = await _unidadTrabajo.Usuario.GetById(id);

            if (usuario != null)
            {
                _unidadTrabajo.Usuario.Remover(usuario);
                await _unidadTrabajo.Guardar();
                return true;
            }

            return false;
        }
    }


}
