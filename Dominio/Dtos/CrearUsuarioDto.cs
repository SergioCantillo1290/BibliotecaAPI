using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Dtos
{
    public class CrearUsuarioDto
    {
        public string Identificacion { get; set; }
        public string NombreCompleto { get; set; }
        public string Email { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Genero { get; set; }
        public int TipoUsuario { get; set; }
    }
}
