using Repositorio.Interfaces;
using Dominio.Entities;
using Repositorio.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio
{
    
    public class PrestamoRepository : IPrestamoRepository
    {
        private readonly DbContexto _context;

        public PrestamoRepository(DbContexto context)
        {

            _context = context;
        }

        public Prestamo Consultar(int id)
        {
            return _context.Prestamos.Find(id);
        }

       

        public Prestamo Consultar(string isbn)
        {
            return _context.Prestamos.FirstOrDefault(x => x.ISBN.Equals(isbn));
        }


        public bool Registrar(Prestamo prestamo)
        {
            _context.Add(prestamo);
            return _context.SaveChanges() > 0;
        }

        public List<Prestamo> ConsultarPorUsuario(int usuarioId)
        {
            return _context.Prestamos.Where(p => p.UsuarioId == usuarioId).ToList();
        }

        public void Eliminar(int id)
        {
            var usuario = _context.Usuarios.Find(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                _context.SaveChanges();
            }
        }




    }


}
