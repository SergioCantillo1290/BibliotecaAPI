using Dominio.Dtos;
using Dominio.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Servicio.Interfaces;

namespace Biblioteca.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UsuarioCreadoDto>> ConsultarTodos()
        {
            var usuarios = _usuarioService.ConsultarTodos();
            return Ok(usuarios);
        }


        [HttpGet("{id}")]
        public ActionResult<Usuario> Consultar(int id)
        {
            return Ok(_usuarioService.Consultar(id));
        }

        [HttpPost]
        public ActionResult Crear([FromBody] CrearUsuarioDto crearUsuarioDto)
        {

            if (!String.IsNullOrEmpty(crearUsuarioDto.Identificacion)
                || !String.IsNullOrEmpty(crearUsuarioDto.NombreCompleto)
                || !String.IsNullOrEmpty(crearUsuarioDto.Email)
                || !String.IsNullOrEmpty(crearUsuarioDto.FechaNacimiento.ToString("yyyy-MM-dd"))
                || !String.IsNullOrEmpty(crearUsuarioDto.Genero)
                || crearUsuarioDto.TipoUsuario != 0)
            {

                var usuarioCreado = _usuarioService.Registrar(crearUsuarioDto);
                if (!EsTipoUsuarioValido(crearUsuarioDto.TipoUsuario))
                {
                    return BadRequest("Tipo de usuario no válido");
                }

                if (usuarioCreado == null)
                {
                    return BadRequest($"El usuario {crearUsuarioDto.Identificacion} ya habia sido creado");
                }

                return Ok(usuarioCreado);


            }
            else
            {

                return BadRequest("Error en los datos de entrada");
            }
       

        }

        [HttpPatch("{id}")]
        public ActionResult Actualizar(int id, [FromBody] CrearUsuarioDto crearUsuarioDto)
        {

            if (string.IsNullOrEmpty(crearUsuarioDto.Email) || crearUsuarioDto.TipoUsuario == 0)
            {
                return BadRequest("Datos de entrada no válidos");
            }

 
            if (!EsTipoUsuarioValido(crearUsuarioDto.TipoUsuario))
            {
                return BadRequest("Tipo de usuario no válido");
            }

            var usuarioActualizado = _usuarioService.Actualizar(id, crearUsuarioDto.Email, crearUsuarioDto.TipoUsuario);

            if (usuarioActualizado == null)
            {
                return BadRequest("Usuario no encontrado o tipo de usuario no válido");
            }

            return BadRequest("Usuario Actualizado");
        }

        [HttpDelete("{id}")]
        public ActionResult Eliminar(int id)
        {
            var resultado = _usuarioService.Eliminar(id);
            if (!resultado)
            {
                return BadRequest("Usuario no encontrado");
            }

            return BadRequest("Usuario Eliminado");
        }


        private bool EsTipoUsuarioValido(int tipoUsuario)
        {
            var tiposValidos = new[] { 1, 2, 3 }; 
            return tiposValidos.Contains(tipoUsuario);
        }


      

    }
}
