using Microsoft.AspNetCore.Mvc;
using Rigid.Models;

namespace Rigid.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private static List<Producto> productos = new List<Producto>(); // Base de datos simulada

        /// <summary>
        /// Obtiene una lista de todos los productos.
        /// </summary>
        /// <returns>Una lista de productos.</returns>
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            // Simulación de datos
            var products = new List<Producto>
            {
                new Producto { Id = 1, Name = "Laptop", SerialNumber = "SN123", Brand = "Dell" },
                new Producto { Id = 2, Name = "Smartphone", SerialNumber = "SN456", Brand = "Apple" }
            };
            return Ok(products);
        }

        /// <summary>
        /// Crea un nuevo producto.
        /// </summary>
        /// <param name="producto">Datos del producto a crear.</param>
        /// <returns>El producto creado.</returns>
        [HttpPost("create")]
        public IActionResult CreateProduct([FromBody] Producto producto)
        {
            productos.Add(producto);
            return Ok(new { message = "Producto creado correctamente.", producto });
        }

        /// <summary>
        /// Busca un producto por su número de serie.
        /// </summary>
        /// <param name="serialNumber">Número de serie del producto.</param>
        /// <returns>El producto encontrado.</returns>
        [HttpGet("search/{serialNumber}")]
        public IActionResult SearchProductBySerialNumber(string serialNumber)
        {
            var product = productos.FirstOrDefault(p => p.SerialNumber == serialNumber);
            if (product == null)
                return NotFound(new { message = "Producto no encontrado." });

            return Ok(product);
        }

        /// <summary>
        /// Elimina un producto por su ID.
        /// </summary>
        /// <param name="id">ID del producto a eliminar.</param>
        /// <returns>Mensaje de confirmación.</returns>
        [HttpDelete("delete/{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = productos.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound(new { message = "Producto no encontrado." });

            productos.Remove(product);
            return Ok(new { message = "Producto eliminado correctamente." });
        }
    }
}