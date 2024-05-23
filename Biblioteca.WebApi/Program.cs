using Repositorio.Data;
using Repositorio.Interfaces;
using Servicio.Interfaces;
using Servicio;
using Microsoft.EntityFrameworkCore;
using Servicio.Mapeo;
using Repositorio;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("conex");
//Inyecciones de dependencias
builder.Services.AddDbContext<DbContexto>(opt => opt.UseSqlServer(connectionString));
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddScoped<ILibroRepository, LibroRepository>();
builder.Services.AddScoped<ILibroService, LibroService>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IPrestamoRepository, PrestamoRepository>();
builder.Services.AddScoped<IPrestamoService, PrestamoService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
