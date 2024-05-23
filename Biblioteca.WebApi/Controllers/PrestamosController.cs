using Dominio.Dtos;
using Dominio.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servicio;
using Servicio.Interfaces;

namespace Biblioteca.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestamosController : ControllerBase
    {
        private readonly IPrestamoService _prestamoService;
        private readonly IUsuarioService _usuarioService;
        private readonly ILibroService _libroService;
        public PrestamosController(IPrestamoService prestamoService , IUsuarioService usuarioService, ILibroService libroService)
        {
            _prestamoService = prestamoService;
            _usuarioService = usuarioService;
            _libroService = libroService;
        }



        [HttpGet("{id}")]
        public ActionResult<Prestamo> Consultar(int id)
        {
            return Ok(_prestamoService.Consultar(id));
        }

        [HttpPost]
        public ActionResult RegistrarPrestamo([FromBody] CrearPrestamoDto crearPrestamoDto)
        {
            // Verificar si los campos necesarios están presentes
            if (string.IsNullOrEmpty(crearPrestamoDto.ISBN) || string.IsNullOrEmpty(crearPrestamoDto.IdentificacionUsuario))
            {
                return BadRequest("Error en los datos de entrada");
            }

            // Verificar si el ISBN existe
            var libro = _libroService.ConsultarISBN(crearPrestamoDto.ISBN);
            if (libro == null)
            {
                return BadRequest($"El libro con ISBN {crearPrestamoDto.ISBN} no existe");
            }

            // Verificar si la identificación del usuario existe
            var usuario = _usuarioService.ConsultarIdentificacion(crearPrestamoDto.IdentificacionUsuario);
            if (usuario == null)
            {
                return BadRequest($"El usuario con identificación {crearPrestamoDto.IdentificacionUsuario} no existe");
            }

            // Verificar si el usuario ya tiene un préstamo realizado
            var prestamoExistente = _prestamoService.Consultar(usuario.Id);
            if (prestamoExistente != null)
            {
                return BadRequest($"El usuario con identificación {crearPrestamoDto.IdentificacionUsuario} ya tiene un préstamo activo");
            }

            // Registrar el préstamo
            var prestamoCreado = _prestamoService.Registrar(crearPrestamoDto);

            if (prestamoCreado == null)
            {
                return BadRequest($"Error al registrar el préstamo");
            }

            return Ok(prestamoCreado);
        }

        [HttpDelete("{id}")]
        public ActionResult Eliminar(int id)
        {
            var resultado = _prestamoService.Eliminar(id);
            if (!resultado)
            {
                return BadRequest("Prestamo no encontrado");
            }

            return BadRequest("Prestamo no econtrado");
        }

    }
}
