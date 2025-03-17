using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using microservicio.Data;
using microservicio.Models;

namespace microservicio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductoesController(AppDbContext context)
        {
            _context = context;
        }

        // Endpoint para insertar un producto usando SP
        [HttpPost]
        public async Task<IActionResult> InsertarProducto([FromBody] Producto producto)
        {
            await _context.InsertarProducto(producto.Nombre, producto.Precio);
            return Ok("Producto insertado correctamente.");
        }

        // Endpoint para obtener todos los productos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> ObtenerProductos()
        {
            var productos = await _context.ConsultarProductos();
            return Ok(productos);
        }

        // Endpoint para eliminar un producto
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarProducto(int id)
        {
            await _context.EliminarProducto(id);
            return Ok("Producto eliminado correctamente.");
        }

        // Endpoint para obtener los últimos 5 productos insertados
        [HttpGet("ultimos5")]
        public async Task<ActionResult<IEnumerable<Producto>>> ObtenerUltimos5()
        {
            var productos = await _context.ObtenerUltimos5Productos();
            return Ok(productos);
        }
    }
}
