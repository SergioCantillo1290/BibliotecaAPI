using Dominio.Dtos;
using Dominio.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Servicio;
using Servicio.Interfaces;

namespace Biblioteca.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private readonly ILibroService _libroService;
        public LibrosController(ILibroService libroService)
        {
            _libroService = libroService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<LibroCreadoDto>> ConsultarTodos()
        {
            var libros = _libroService.ConsultarTodos();
            return Ok(libros);
        }


        [HttpGet("{id}")]
        public ActionResult<Libro> Consultar(int id)
        {
            return Ok(_libroService.Consultar(id));
        }

        [HttpPost]
        public ActionResult Crear([FromBody] CrearLibroDto crearLibroDto)
        {
            


            if (!String.IsNullOrEmpty(crearLibroDto.ISBN)
                || !String.IsNullOrEmpty(crearLibroDto.Titulo)
                || !String.IsNullOrEmpty(crearLibroDto.Autor)
                || !String.IsNullOrEmpty(crearLibroDto.Editorial)
                || !String.IsNullOrEmpty(crearLibroDto.FechaPublicacion.ToString("yyyy-MM-dd"))
                || !String.IsNullOrEmpty(crearLibroDto.Genero)
                || !String.IsNullOrEmpty(crearLibroDto.Sinopsis)
                || !String.IsNullOrEmpty(crearLibroDto.Idioma))
            {

                var libroCreado = _libroService.Registrar(crearLibroDto);

                if (libroCreado == null)
                {
                    return BadRequest($"El libro que contiene ese titulo {crearLibroDto.Titulo} ya habia sido creado");
                }

                return Ok(libroCreado);


            }
            else
            {

                return BadRequest("Error en los datos de entrada");
            }         
        }

        [HttpPatch("{id}")]
        public ActionResult Actualizar(int id, [FromBody] CrearLibroDto crearLibroDto)
        {

            if (string.IsNullOrEmpty(crearLibroDto.Sinopsis))
            {
                return BadRequest("Por favor ingresa la descripcion de la Sinopsis");
            }

            var libroActualizado = _libroService.Actualizar(id, crearLibroDto.Sinopsis);

            if (libroActualizado == null)
            {
                return BadRequest("Libro no encontrado");
            }

            return BadRequest("Libro Actualizado");
        }

        [HttpDelete("{id}")]
        public ActionResult Eliminar(int id)
        {
            var resultado = _libroService.Eliminar(id);
            if (!resultado)
            {
                return BadRequest("Libro no encontrado");
            }

            return BadRequest("Libro Borrado");
        }
    }
}
