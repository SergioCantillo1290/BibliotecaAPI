using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Dtos
{
    public class CrearPrestamoDto
    {

        public string ISBN { get; set; }
        public string IdentificacionUsuario { get; set; }

        public int UsuarioId { get; set; }
    }
}
