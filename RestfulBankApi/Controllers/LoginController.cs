﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestfulBankApi.Authentication;
using RestfulBankApi.Data;
using RestfulBankApi.Models;

namespace RestfulBankApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly JwtAuthenticationManager jwtAuthenticationManager;
        private readonly AppDbContext _context;
        public LoginController(JwtAuthenticationManager jwtAuthenticationManager, AppDbContext context)
        {
            this.jwtAuthenticationManager = jwtAuthenticationManager;
            _context = context;
        }


        [AllowAnonymous]
        [HttpPost("Authorize")]
        public IActionResult AuthUser(LoginModel usr)
        {
            List<User> users = _context.Users.ToList();

            var token = jwtAuthenticationManager.Authenticate(usr.UserName, usr.PasswordHash, users);
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }

        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Policy = "AdminPolicy")]
        [Route("TestRoute")]
        [HttpGet]
        public IActionResult test()
        {
            return Ok("Authorized");
        }
    }
}
