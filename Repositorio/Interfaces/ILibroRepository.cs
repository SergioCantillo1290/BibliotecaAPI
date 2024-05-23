using Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio.Interfaces
{
    public interface ILibroRepository
    {
        bool Registrar(Libro libro);
        Libro Consultar(int id);
        Libro Consultar(string iSBN);
        void Actualizar(Libro libro);
        void Eliminar(int id);
        IEnumerable<Libro> ConsultarTodos();

        Libro ConsultarISBN(string isbn);
    }
}
