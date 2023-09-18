using API.Especificaciones;
using AutoMapper;
using Core.DTO;
using Core.Entidades;
using Core.Negocio.INegocio;
using Infraestructura.Data.Repositorio;
using Infraestructura.Data.Repositorio.IRepositorio;
using Infraestructura.Data.Seeds;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.Entidades.Usuario;

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

        public async Task<IEnumerable<UsuarioReedDTO>> GetAllUsuarios(int pageNumber, int pageSize)
        {
            var parametros = new Parametros
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var paginaUsuarios = await _unidadTrabajo.Usuario.ObtenerTodosPaginado(parametros);
            return _mapper.Map<IEnumerable<UsuarioReedDTO>>(paginaUsuarios);

        }

        public async Task<UsuarioReedDTO> GetUsuarioById(int id)
        {
            var usuario = await _unidadTrabajo.Usuario.GetById(id);
            if (usuario == null)
            {
                return null;
            }
            return _mapper.Map<UsuarioReedDTO>(usuario);

        }

        public async Task<bool> CrearUsuario(UsuarioReedDTO usuarioDTO)
        {
            var existeUsuario = await _unidadTrabajo.Usuario.Existe(p => p.NombreCompleto == usuarioDTO.NombreCompleto);

            if (existeUsuario)
            {
                throw new Exception("Ya existe un usuario con ese nombre.");
            }
            var usuario = _mapper.Map<Usuario>(usuarioDTO);
            usuario.Contrasenia = PasswordEncryptHelper.EncryptPassword(usuario.Contrasenia);
            await _unidadTrabajo.Usuario.Agregar(usuario);
            await _unidadTrabajo.Guardar();
            return true;
        }



        public async Task<bool> ActualizarUsuario(UsuarioDTO usuarioDTO)
        {
            var usuarioExiste = await _usuarioRepositorio.ObtenerPrimero(t => t.Id == usuarioDTO.Id);
            if (usuarioExiste != null)
            {
                if (!string.IsNullOrEmpty(usuarioDTO.NombreCompleto))
                {
                    usuarioExiste.NombreCompleto = usuarioDTO.NombreCompleto;
                }

                if (usuarioDTO.Dni > 0)
                {
                    usuarioExiste.Dni = usuarioDTO.Dni;
                }


                if (!string.IsNullOrEmpty(usuarioDTO.Tipo))
                {
                    if (Enum.TryParse(typeof(TipoUsuario), usuarioDTO.Tipo, out var tipoUsuario))
                    {
                        usuarioExiste.Tipo = (TipoUsuario)tipoUsuario;
                    }
                    else
                    {
                        throw new Exception("El valor del tipo de usuario no es válido.");
                    }
                }


                _usuarioRepositorio.Actualizar(usuarioExiste);
                await _unidadTrabajo.Guardar();
                return true;
            }
            else
            {
                throw new Exception("El Usuario no existe en la base de datos.");
            }
        }

        public async Task<bool> EliminarUsuario(int id)
        {
            var usuario = await _unidadTrabajo.Usuario.GetById(id);

            if (usuario != null)
            {
                await _unidadTrabajo.Usuario.Eliminar(id);
                await _unidadTrabajo.Guardar();
                return true;
            }

            return false;
        }
    }


}
