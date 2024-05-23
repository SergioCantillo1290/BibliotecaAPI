using Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio.Interfaces
{
    public interface IUsuarioRepository
    {
        void Actualizar(Usuario usuario);
        void Eliminar(int id);
        bool Registrar(Usuario usuario);
        Usuario Consultar(int id);
        Usuario Consultar(string Indentificacion);

        IEnumerable<Usuario> ConsultarTodos();

        Usuario ConsultarIdentificacion(string identificacion);


    }
}
