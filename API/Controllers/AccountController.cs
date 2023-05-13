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
    private IAccounts _accounts;

    public AccountController(IAccounts accounts)
    {
        _accounts = accounts;
    }


    [HttpGet("/registration")]
    public IActionResult RegistrationGet() => NotFound();
    
    [HttpPost("/registration")]
    public IActionResult RegistrationPost(Registration registrationInfo)
    {
        var user = _accounts.Registration(registrationInfo.Name, registrationInfo.Password,
            registrationInfo.Surname, registrationInfo.Number);
        if (user is null)
            return StatusCode(406);
        return Redirect($"/User/{user.Id}");
    }

    [HttpGet("/login")]
    public IActionResult LoginGet() => NotFound();

    [HttpPost("/login")]
    public async Task<IActionResult> LoginPost(Login login)
    {
        User user = _accounts.Auth(login.Name, login.Password);
        if (user is null) return Unauthorized();
        var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Name) };
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
    public async Task<IActionResult> UserGet(int id)
    {
        return Ok("Hello world, " + id);
    }
}