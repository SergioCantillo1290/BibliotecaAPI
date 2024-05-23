using Dominio.Entities;
using Repositorio.Interfaces;
using Repositorio.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DbContexto _context;

        public UsuarioRepository(DbContexto context)
        {

            _context = context;
        }

        public Usuario ConsultarIdentificacion(string identificacion)
        {
            return _context.Usuarios.SingleOrDefault(u => u.Identificacion == identificacion);
        }


        public Usuario Consultar(int id)
        {
            return _context.Usuarios.Find(id);
        }

        public Usuario Consultar(string identifacion)
        {
            return _context.Usuarios.FirstOrDefault(x => x.Identificacion.Equals(identifacion));
        }


        public bool Registrar(Usuario usuario)
        {
            _context.Add(usuario);
            return _context.SaveChanges() > 0;
        }

        public void Actualizar(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            _context.SaveChanges();
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

        public IEnumerable<Usuario> ConsultarTodos()
        {
            return _context.Usuarios.ToList();
        }
    }
}
