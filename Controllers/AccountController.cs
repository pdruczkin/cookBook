using cookBook.Models;
using cookBook.Services;
using Microsoft.AspNetCore.Mvc;

namespace cookBook.Controllers
{
    [ApiController]
    [Route("api/cookbook/account")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _service;

        public AccountController(IAccountService service)
        {
            _service = service;
        }


        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody] RegisterUserDto dto)
        {
            _service.RegisterUser(dto);
            return Ok();
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginDto dto)
        {
            var token = _service.GenerateJwtToken(dto);

            return Ok(token);

        }
    }
}