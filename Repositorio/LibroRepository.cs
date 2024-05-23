using Repositorio.Interfaces;
using Dominio.Entities;
using Repositorio.Data;


namespace Repositorio
{
    public class LibroRepository : ILibroRepository
    {
        private readonly DbContexto _context;

        public LibroRepository(DbContexto context)
        {

            _context = context;
        }

        public Libro ConsultarISBN(string isbn)
        {
            return _context.Libros.SingleOrDefault(u => u.ISBN == isbn);
        }

        public Libro Consultar(int id)
        {
            return _context.Libros.Find(id);
        }

        public Libro Consultar(string ISBN)
        {
            return _context.Libros.FirstOrDefault(x => x.ISBN.Equals(ISBN));
        }

     
        public bool Registrar(Libro libro)
        {
            _context.Add(libro);
            return _context.SaveChanges() > 0;
        }
        public void Actualizar(Libro libro)
        {
            _context.Libros.Update(libro);
            _context.SaveChanges();
        }

        public void Eliminar(int id)
        {
            var libro = _context.Libros.Find(id);
            if (libro != null)
            {
                _context.Libros.Remove(libro);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Libro> ConsultarTodos()
        {
            return _context.Libros.ToList();
        }
    }
}
