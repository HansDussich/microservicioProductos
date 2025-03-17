using microservicio.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace microservicio.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Producto> Productos { get; set; }

        //Insertar SP
        public async Task InsertarProducto(String nombre, decimal precio)
        {
            await Database.ExecuteSqlRawAsync("EXEC InsertarProducto @p0, @p1", nombre, precio);
        }
        //Llamar SP de Consulta
        public async Task<List<Producto>> ConsultarProductos()
        {
            return await Productos.FromSqlRaw("EXEC ConsultarProductos").ToListAsync();
        }

        // Llamar al SP de eliminación
        public async Task EliminarProducto(int id)
        {
            await Database.ExecuteSqlRawAsync("EXEC EliminarProducto @p0", id);
        }

        // Llamar al SP para obtener los últimos 5 registros
        public async Task<List<Producto>> ObtenerUltimos5Productos()
        {
            return await Productos.FromSqlRaw("EXEC ObtenerUltimos5Productos").ToListAsync();
        }
    }
}
