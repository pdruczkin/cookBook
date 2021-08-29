﻿using cookBook.Models;
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

    }
}