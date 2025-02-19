using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Rigid.Services;
using System.Threading.Tasks;
namespace Rigid.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var token = await _authService.GetTokenAsync(request.Username, request.Password);
                return Ok(new { Token = token });
            }
            catch
            {
                return Unauthorized("Credenciales inválidas");
            }
        }
    }

    //test

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
