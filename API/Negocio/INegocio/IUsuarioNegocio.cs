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
        Task<IEnumerable<UsuarioReedDTO>> GetAllUsuarios(int pageNumber, int pageSize);
        Task<UsuarioReedDTO> GetUsuarioById(int id);
        Task<bool> CrearUsuario(UsuarioReedDTO usuarioDTO);
        Task<bool> ActualizarUsuario(UsuarioDTO usuarioDTO);
        Task<bool> EliminarUsuario(int id);
    }
}
