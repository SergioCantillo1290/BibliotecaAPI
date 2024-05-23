using Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio.Interfaces
{
    public interface IPrestamoRepository
    {
        bool Registrar(Prestamo prestamo);
        Prestamo Consultar(int id);
        Prestamo Consultar(string ISBN);
        void Eliminar(int id);
        List<Prestamo> ConsultarPorUsuario(int usuarioId);
        


    }
}
