using AutoMapper;
using Dominio.Dtos;
using Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.Mapeo
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CrearLibroDto, Libro>();
            CreateMap<Libro, LibroCreadoDto>();
            CreateMap<CrearPrestamoDto, Prestamo>();
            CreateMap<Prestamo, PrestamoCreadoDto>();
            CreateMap<CrearUsuarioDto, Usuario>();
            CreateMap<Usuario, UsuarioCreadoDto>();
            CreateMap<ListaLibrosDto, Libro>();
            CreateMap<ListaUsuariosDto, Usuario>();
            CreateMap<ListaPrestamosDto, Prestamo>();

        }
    }
}
