using AutoMapper;
using Dominio.Dtos;
using Dominio.Entities;
using Servicio.Interfaces;
using Repositorio.Interfaces;

namespace Servicio
{ 
public class LibroService : ILibroService
{
        private readonly ILibroRepository _repository;
        private readonly IMapper _mapper;



        public LibroService(ILibroRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;

        }

        public Libro ConsultarISBN(string isbn)
        {
            return _repository.ConsultarISBN(isbn);
        }

        public Libro Consultar(int id)
        {

            return _repository.Consultar(id);
        }



        public LibroCreadoDto Registrar(CrearLibroDto dto)
        {


            var existe = _repository.Consultar(dto.ISBN);

            if (existe is null)
            {

                var libro = _mapper.Map<Libro>(dto);
                libro.FechaRegistro = DateTime.Now;
                libro.Editorial = libro.Editorial.Substring(0, 1).ToUpper() + new Random().NextInt64(1, 10000);


                    _repository.Registrar(libro);

                    var libroCreado = _mapper.Map<LibroCreadoDto>(libro);

                    return libroCreado;             

            }

            return null;
        }

        public IEnumerable<LibroCreadoDto> ConsultarTodos()
        {
            var libros = _repository.ConsultarTodos();
            return _mapper.Map<IEnumerable<LibroCreadoDto>>(libros);
        }

        public LibroCreadoDto Actualizar(int id, string sinopsis)
        {
            var libro = _repository.Consultar(id);

            if (libro == null)
            {
                return null;
            }

          
            libro.Sinopsis = sinopsis;


            _repository.Actualizar(libro);

            var libroActualizado = _mapper.Map<LibroCreadoDto>(libro);
            return libroActualizado;
        }

        public bool Eliminar(int id)
        {
            var libro = _repository.Consultar(id);
            if (libro == null)
            {
                return false;
            }

            _repository.Eliminar(id);
            return true;
        }
    }
}
