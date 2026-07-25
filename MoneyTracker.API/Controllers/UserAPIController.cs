using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyTracker.Application.DTOs.UserDTOs;
using MoneyTracker.Application.Auth.Commands.Login;
using MoneyTracker.Application.Auth.Commands.Register;

namespace MoneyTracker.API.Controllers
{
    [Route("api/UserController")]
    [ApiController]
    [AllowAnonymous]
    public class UserAPIController : ControllerBase
    {
        private readonly IMediator _mediater;
        public UserAPIController(IMediator mediator)
        {
            _mediater = mediator;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterUserDto dto)
        {
            if(dto == null)
            {
                return BadRequest("Invalid user data.");
            }
            var result = await _mediater.Send(new RegisterCommand(dto));
            return Ok(result);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUserDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid user data.");
            }
            var result = await _mediater.Send(new LoginCommand(dto));
            return Ok(result);
        }
       
    }
}
