using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartStudy.Application.UseCases.Auth.Login;
using SmartStudy.Application.UseCases.Auth.Register;
using SmartStudy.Domain.Common;

namespace SmartStudy.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly RegisterUserUseCase _registerUserUseCase;
        private readonly LoginUseCase _loginUseCase;

        public AuthController(RegisterUserUseCase registerUserUseCase, LoginUseCase loginUseCase)
        {
            _registerUserUseCase = registerUserUseCase;
            _loginUseCase = loginUseCase;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _registerUserUseCase.ExecuteAsync(request);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            LoginResponse result = await _loginUseCase.ExecuteAsync(loginRequest);
            
            return Ok(result);
        }
    }
}
