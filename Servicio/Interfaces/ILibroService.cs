using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entities;
using Dominio.Dtos;

namespace Servicio.Interfaces
{
    public interface ILibroService
    {
        LibroCreadoDto Registrar(CrearLibroDto dto);
        Libro Consultar(int id);
        IEnumerable<LibroCreadoDto> ConsultarTodos();
        LibroCreadoDto Actualizar(int id, string sinopsis);
        bool Eliminar(int id);

        Libro ConsultarISBN(string isbn);

    }
}
