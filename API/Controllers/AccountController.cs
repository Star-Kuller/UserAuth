using API.Requsts;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    [HttpGet("/registration")]
    public IActionResult RegistrationGet() => NotFound();
    
    [HttpPost("/registration")]
    public IActionResult RegistrationPost(Registration registrationInfo)
    {
        return BadRequest();
    }
    
    [HttpGet("/login")]
    public IActionResult LoginGet()
    {
        // html-форма для ввода логина/пароля
        string loginForm = @"<!DOCTYPE html>
        <html>
            <head>
                <meta charset='utf-8' />
                <title>METANIT.COM</title>
            </head>
            <body>
                <h2>Login Form</h2>
                <form method='post'>
                    <p>
                        <label>Email</label><br />
                        <input name='email' />
                    </p>
                    <p>
                        <label>Password</label><br />
                        <input type='password' name='password' />
                    </p>
                    <input type='submit' value='Login' />
                </form>
            </body>
        </html>";
        return Ok(loginForm);
    }
}