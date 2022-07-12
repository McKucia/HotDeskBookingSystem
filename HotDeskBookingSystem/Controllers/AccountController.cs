using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotDeskBookingSystem.Entities;
using HotDeskBookingSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotDeskBookingSystem.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAccountService _service;

        public AccountController(IAccountService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public ActionResult RegisterEmployee([FromBody] RegisterEmployeeDto dto)
        {
            var isRegistered = _service.RegisterEmployee(dto);

            if(!isRegistered)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginEmployeeDto dto)
        {
            string token = _service.GenerateJwt(dto);

            if (token == "nouser")
            {
                return NotFound("Email not exist");
            }
            if (token == "invalidpass")
            {
                return NotFound("Invalid Password");
            }

            return Ok(token);
        }
    }
}

