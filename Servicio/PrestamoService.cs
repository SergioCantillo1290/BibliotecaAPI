using AutoMapper;
using Dominio.Dtos;
using Dominio.Entities;
using Repositorio;
using Repositorio.Interfaces;
using Servicio.Interfaces;


namespace Servicio
{
    public class PrestamoService : IPrestamoService
    {
        private readonly IPrestamoRepository _repository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;



        public PrestamoService(IPrestamoRepository repository, IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _mapper = mapper;
            _usuarioRepository = usuarioRepository;
            _repository = repository;

        }

        public Prestamo Consultar(int id)
        {

            return _repository.Consultar(id);
        }

        public PrestamoCreadoDto Registrar(CrearPrestamoDto dto)
        {


            var existe = _repository.Consultar(dto.ISBN);

            if (existe is null)
            {
                var usuario = _usuarioRepository.Consultar(dto.UsuarioId);
                if (usuario == null)
                {
                    return null;
                }

                // Verificar si el usuario ya tiene un libro prestado
                var librosPrestados = _repository.ConsultarPorUsuario(dto.UsuarioId);
                if (librosPrestados.Any())
                {
                    throw new InvalidOperationException("El usuario ya tiene un libro prestado");
                }

                var prestamo = _mapper.Map<Prestamo>(dto);
                prestamo.FechaRegistro = DateTime.Now;
                prestamo.FechaPrestamo = DateTime.Now;


                int diasPrestamo;
                switch (usuario.TipoUsuario)
                {
                    case 1:
                        diasPrestamo = 10;
                        break;
                    case 2:
                        diasPrestamo = 8;
                        break;
                    case 3:
                        diasPrestamo = 7;
                        break;
                    default:
                        throw new ArgumentException("Tipo de usuario no válido");
                }

                prestamo.FechaMaximaDevolucion = CalcularFechaDevolucion(prestamo.FechaPrestamo, diasPrestamo);

                _repository.Registrar(prestamo);

                var prestamoCreado = _mapper.Map<PrestamoCreadoDto>(prestamo);

                return prestamoCreado;
            }

            return null;
        }

        private DateTime CalcularFechaDevolucion(DateTime fechaInicio, int dias)
        {
            int diasAgregados = 0;
            DateTime fechaDevolucion = fechaInicio;

            while (diasAgregados < dias)
            {
                fechaDevolucion = fechaDevolucion.AddDays(1);

                if (fechaDevolucion.DayOfWeek != DayOfWeek.Saturday && fechaDevolucion.DayOfWeek != DayOfWeek.Sunday)
                {
                    diasAgregados++;
                }
            }

            return fechaDevolucion;
        }


        public bool Eliminar(int id)
        {
            var prestamo = _repository.Consultar(id);
            if (prestamo == null)
            {
                return false;
            }

            _repository.Eliminar(id);
            return true;
        }

    }
}
