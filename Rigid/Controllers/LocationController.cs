using Microsoft.AspNetCore.Mvc;
using Rigid.Models;

namespace Rigid.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationsController : ControllerBase
    {
        private static List<Ubicacion> ubicaciones = new List<Ubicacion>(); // Base de datos simulada

        /// <summary>
        /// Obtiene una lista de todas las ubicaciones.
        /// </summary>
        /// <returns>Una lista de ubicaciones.</returns>
        [HttpGet]
        public IActionResult GetAllLocations()
        {
            // Simulación de datos
            var locations = new List<Ubicacion>
            {
                new Ubicacion { Id = 1, City = "Oficina Central", Address = "Calle 123" },
                new Ubicacion { Id = 2, City = "Sucursal Norte", Address = "Avenida 456" }
            };
            return Ok(locations);
        }

        /// <summary>
        /// Crea una nueva ubicación.
        /// </summary>
        /// <param name="ubicacion">Datos de la ubicación a crear.</param>
        /// <returns>La ubicación creada.</returns>
        [HttpPost("create")]
        public IActionResult CreateLocation([FromBody] Ubicacion ubicacion)
        {
            ubicaciones.Add(ubicacion);
            return Ok(new { message = "Ubicación creada correctamente.", ubicacion });
        }

        /// <summary>
        /// Desactiva una ubicación por su ID.
        /// </summary>
        /// <param name="id">ID de la ubicación a desactivar.</param>
        /// <returns>Mensaje de confirmación.</returns>
        [HttpPut("disable/{id}")]
        public IActionResult DisableLocation(int id)
        {
            var location = ubicaciones.FirstOrDefault(l => l.Id == id);
            if (location == null)
                return NotFound(new { message = "Ubicación no encontrada." });

            location.IsActive = false;
            return Ok(new { message = "Ubicación desactivada correctamente." });
        }
    }
}