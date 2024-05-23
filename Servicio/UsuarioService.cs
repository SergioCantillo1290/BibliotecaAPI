using AutoMapper;
using Dominio.Dtos;
using Dominio.Entities;
using Servicio.Interfaces;
using Repositorio.Interfaces;

namespace Servicio
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;
        private readonly IMapper _mapper;



        public UsuarioService(IUsuarioRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;

        }

        public Usuario ConsultarIdentificacion(string identificacion)
        {
            return _repository.ConsultarIdentificacion(identificacion);
        }

        public Usuario Consultar(int id)
        {

            return _repository.Consultar(id);
        }


        public UsuarioCreadoDto Registrar(CrearUsuarioDto dto)
        {
            var existe = _repository.Consultar(dto.NombreCompleto);

            if (existe is null)
            {
                var usuario = _mapper.Map<Usuario>(dto);
                usuario.FechaRegistro = DateTime.Now;

                int tipoUsuario = ValidarTipoUsuario(dto.TipoUsuario);
                if (tipoUsuario == -1)
                {
                    return null;

                }

                usuario.TipoUsuario = tipoUsuario;

                _repository.Registrar(usuario);
                var usuarioCreado = _mapper.Map<UsuarioCreadoDto>(usuario);
                return usuarioCreado;
            }

            return null;
        }

        private int ValidarTipoUsuario(int tipoUsuario)
        {
            
            var tiposValidos = new[] { 1, 2, 3 }; 
            return tiposValidos.Contains(tipoUsuario) ? tipoUsuario : -1;
        }


        public UsuarioCreadoDto Actualizar(int id, string email, int tipoUsuario)
        {
            var usuario = _repository.Consultar(id);

            if (usuario == null)
            {
                return null;
            }

            
            int validTipoUsuario = ValidarTipoUsuario(tipoUsuario);
            if (validTipoUsuario == -1)
            {
                return null; 
            }

            
            usuario.Email = email;
            usuario.TipoUsuario = validTipoUsuario;

            _repository.Actualizar(usuario);

            var usuarioActualizado = _mapper.Map<UsuarioCreadoDto>(usuario);
            return usuarioActualizado;
        }

        public bool Eliminar(int id)
        {
            var usuario = _repository.Consultar(id);
            if (usuario == null)
            {
                return false;
            }

            _repository.Eliminar(id);
            return true;
        }

        public IEnumerable<UsuarioCreadoDto> ConsultarTodos()
        {
            var usuarios = _repository.ConsultarTodos();
            return _mapper.Map<IEnumerable<UsuarioCreadoDto>>(usuarios);
        }

    }
}
