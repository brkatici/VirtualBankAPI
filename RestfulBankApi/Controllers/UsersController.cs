using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Generators;
using RestfulBankApi.Authentication;
using RestfulBankApi.Data;
using RestfulBankApi.DTOs;
using RestfulBankApi.Interfaces;
using RestfulBankApi.Models;

namespace RestfulBankApi.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser(UserDto model)
        {
   
            var result = await _userService.AddUserAsync(model);
            return Ok(result);
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(string email, string newPassword, string newPasswordAgain)
        {
            var result = await _userService.ResetPasswordAsync(email, newPassword, newPasswordAgain);
            return result == "Password reset successfully." ? Ok(result) : BadRequest(result);
        }

        //[HttpGet("GetSpecificUser/{userId}")]
        //public async Task<IActionResult> GetUserById(int userId)
        //{
        //    var user = await _userService.GetUserByIdAsync(userId);
        //    return user != null ? Ok(user) : NotFound();
        //}

        //[HttpGet("GetAllUsers")]
        //public async Task<IActionResult> GetAllUsers()
        //{
        //    var users = await _userService.GetAllUsersAsync();
        //    return Ok(users);
        //}

    }
}
