using Dominio.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repositorio.Data
{
    public class DbContexto(DbContextOptions<DbContexto> opt) : DbContext(opt)
    {
        public DbSet<Libro> Libros { get; set; }
        public DbSet<Prestamo> Prestamos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

    }
}
