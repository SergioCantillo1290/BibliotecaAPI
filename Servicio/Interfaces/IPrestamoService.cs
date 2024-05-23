using Dominio.Entities;
using Dominio.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.Interfaces
{
    public interface IPrestamoService
    {
        PrestamoCreadoDto Registrar(CrearPrestamoDto dto);
        Prestamo Consultar(int id);
        bool Eliminar(int id);

    }
}
