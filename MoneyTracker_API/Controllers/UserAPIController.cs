using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoneyTracker_API.DTOs;
using MoneyTracker_API.Models;
using MoneyTracker_API.ServiceContracts;
using System.Net;

namespace MoneyTracker_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAPIController : Controller
    {
        private readonly IAuthService _authService;
        protected APIResponse _response;
        public UserAPIController(IAuthService authService)
        {
            _authService = authService;
            this._response = new();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
        {
            LoginResponseDTO user = await _authService.Login(model);
            if(user == null || string.IsNullOrEmpty(user.Token))
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Username or password is incorrect");
                return BadRequest(_response);
            }
            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            _response.Result = user;
            return Ok(_response);
        }
        [HttpPut("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterationRequestDTO model)
        {
            var user = await _authService.Register(model);
            if (user == null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Username or Password or Role is null");
                return BadRequest(_response);
            }
            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            _response.Result = user;
            return Ok(_response);
        }
    }
}
