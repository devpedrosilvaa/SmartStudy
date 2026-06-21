using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartStudy.Application.UseCases.Auth.Register;

namespace SmartStudy.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly RegisterUserUseCase _registerUserUseCase;

        public AuthController(RegisterUserUseCase registerUserUseCase)
        {
            _registerUserUseCase = registerUserUseCase;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _registerUserUseCase.ExecuteAsync(request);
            return Ok();
        }
    }
}
