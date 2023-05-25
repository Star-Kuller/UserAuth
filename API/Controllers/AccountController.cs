using System.Security.Claims;
using API.Requsts;
using Core.Entities;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiController]
//[Route("[controller]")]
[Route("/")]
public class AccountController : ControllerBase
{
    private readonly IAccounts _accounts;
    private readonly IRepository _repository;

    public AccountController(IAccounts accounts, IRepository repository)
    {
        _accounts = accounts;
        _repository = repository;
    }


    [HttpGet("/registration")]
    public IActionResult RegistrationGet() => BadRequest("Work In Progress (/registration)");
    
    [HttpPost("/registration")]
    public async Task<IActionResult> RegistrationPost(Registration registrationInfo)
    {
        var user = await _accounts.Registration(registrationInfo.Name, registrationInfo.Password,
            registrationInfo.Surname, registrationInfo.Number);
        if (user is null)
            return BadRequest("User already exist");
        return Redirect($"/User/{user.Id}");
    }

    [HttpGet("/login")]
    public IActionResult LoginGet() => BadRequest("Work In Progress (/login)");

    [HttpPost("/login")]
    public async Task<IActionResult> LoginPost(Login login)
    {
        var user = _accounts.Auth(login.Name, login.Password);
        if (user is null) return Unauthorized();
        var claims = new List<Claim>
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
        };
        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        return Redirect($"/User/{user.Id}");
    }
    
    [HttpGet("/logout")]
    public async Task<IActionResult> LogoutGet()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Redirect("/login");
    }
    
    [Authorize]
    [HttpGet("/User/{id?}")]
    public IActionResult UserGet(long id)
    {
        var user = _repository.GetUser(id);
        if (user is null)
            return BadRequest();
        if (HttpContext.User.Identity.Name == user.Name)
            return Ok("I'm " + id);
        return Ok("Hello, " + id);
    }

    [Authorize]
    [HttpDelete("/User/{id?}")]
    public async Task<IActionResult> UserDelete(long id)
    {
        var user = _repository.GetUser(id);
        if (user is null)
            return BadRequest();
        if (HttpContext.User.Identity.Name != user.Name)
            return StatusCode(403);
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        _repository.DeleteUser(_repository.GetUser(id));
        return Ok();
    }
    

    [Authorize]
    [HttpGet("/User")]
    public IActionResult UserGet() => BadRequest("Work In Progress (/User)");
    
    [Authorize]
    [HttpGet("/UsersList")]
    public IActionResult UsersListGet()
    {
        return Ok(_repository.GetAllUsers());
    }
}