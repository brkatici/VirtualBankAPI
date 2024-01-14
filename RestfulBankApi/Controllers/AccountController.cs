using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using RestfulBankApi;
using RestfulBankApi.Data;
using RestfulBankApi.DTOs;
using RestfulBankApi.Interfaces;
using RestfulBankApi.Models;
using System.Security.Claims;
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
[Route("api/accounts")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }
    private string GetUserIdFromContext()
    {
        return HttpContext.User.FindFirst("UserId")?.Value ?? string.Empty;
    }

    
    [HttpPost("create")]
    public async Task<IActionResult> CreateAccount([FromBody] AccountCreationModel model)
    {
        var userId = GetUserIdFromContext();
        try
        {
            var account = await _accountService.CreateAccountAsync(model, int.Parse(userId));
            return Ok("Account created successfully.");
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("user-accounts")]
    public async Task<IActionResult> GetUserAccounts()
    {
        var userId = GetUserIdFromContext();
        var accounts = await _accountService.GetUserAccountsAsync(userId);
        return Ok(accounts);
    }
}
