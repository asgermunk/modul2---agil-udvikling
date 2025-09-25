using Microsoft.AspNetCore.Mvc;

namespace modul2_agiludvikling.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Login endpoint - checks if email and password are correct
        /// </summary>
        /// <param name="request">Login credentials</param>
        /// <returns>LoginResponse with success status and message</returns>
        [HttpPost("login")]
        public ActionResult<LoginResponse> Login([FromBody] LoginRequest request)
        {
            if (request == null)
            {
                return BadRequest(new LoginResponse
                {
                    Success = false,
                    Message = "Login request is required."
                });
            }

            var result = _authService.Login(request);
            
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return Unauthorized(result);
            }
        }
    }
}