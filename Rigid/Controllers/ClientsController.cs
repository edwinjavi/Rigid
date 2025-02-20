using Microsoft.AspNetCore.Mvc;
using Rigid.Models;

namespace Rigid.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private static List<Cliente> clientes = new List<Cliente>(); // Base de datos simulada

        /// <summary>
        /// Obtiene una lista de todos los clientes.
        /// </summary>
        /// <returns>Una lista de clientes.</returns>
        [HttpGet]
        public IActionResult GetAllClients()
        {
            // Simulación de datos
            var clients = new List<Cliente>
            {
                new Cliente { Id = 1, Name = "Juan", Email = "juan@example.com" },
                new Cliente { Id = 2, Name = "Maria", Email = "maria@example.com" }
            };
            return Ok(clients);
        }

        /// <summary>
        /// Crea un nuevo cliente.
        /// </summary>
        /// <param name="cliente">Datos del cliente a crear.</param>
        /// <returns>El cliente creado.</returns>
        [HttpPost("create")]
        public IActionResult CreateClient([FromBody] Cliente cliente)
        {
            clientes.Add(cliente);
            return Ok(new { message = "Cliente creado correctamente.", cliente });
        }

        /// <summary>
        /// Desactiva un cliente por su ID.
        /// </summary>
        /// <param name="id">ID del cliente a desactivar.</param>
        /// <returns>Mensaje de confirmación.</returns>
        [HttpPut("disable/{id}")]
        public IActionResult DisableClient(int id)
        {
            var client = clientes.FirstOrDefault(c => c.Id == id);
            if (client == null)
                return NotFound(new { message = "Cliente no encontrado." });

            client.IsActive = false;
            return Ok(new { message = "Cliente desactivado correctamente." });
        }
    }
}