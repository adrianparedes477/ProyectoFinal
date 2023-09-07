using Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Negocio.INegocio
{
    public interface IUsuarioNegocio
    {
        Task<IEnumerable<UsuarioDTO>> GetAllUsuarios();
        Task<UsuarioDTO> GetUsuarioById(int id);
        Task<bool> CrearUsuario(UsuarioDTO usuarioDTO);
        Task<bool> ActualizarUsuario(UsuarioDTO usuarioDTO);
        Task<bool> EliminarUsuario(int id);
    }
}
