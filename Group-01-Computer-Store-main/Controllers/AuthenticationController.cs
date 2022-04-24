using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Group1CompStore.Data;
using Group1CompStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Group1CompStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        private readonly JwtAuthenticationManager jwtAuthenticationManager;

        public AuthenticationController(JwtAuthenticationManager jwtAuthenticationManager)
        {
            this.jwtAuthenticationManager = jwtAuthenticationManager;
        }

        [AllowAnonymous]
        [HttpPost("Authorize")]
        public IActionResult AuthUser([FromBody] User usr)
        {
            var token = jwtAuthenticationManager.Authenticate(usr.username, usr.password);
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }

        [Authorize]
        [Route("TestRoute")]
        [HttpGet]
        public IActionResult test()
        {
            return Ok("Authorized");
        }
    }
    public class User
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}