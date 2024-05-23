using Dominio.Entities;
using Dominio.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.Interfaces
{
    public interface IUsuarioService
    {
        UsuarioCreadoDto Registrar(CrearUsuarioDto dto);
        Usuario Consultar(int id);

        bool Eliminar(int id);
        UsuarioCreadoDto Actualizar(int id, string email, int tipoUsuario);
        IEnumerable<UsuarioCreadoDto> ConsultarTodos();

        Usuario ConsultarIdentificacion(string identificacion);

    }
}
