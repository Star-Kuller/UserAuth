using API.Requsts;
using Infrastructure;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiController]
[Route("[controller]")]
public class HobbyController : ControllerBase
{
    private readonly IRepository _repository;

    public HobbyController(IRepository repository)
    {
        _repository = repository;
    }
    
    [Authorize]
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_repository.GetAllHobbies());
    }
    
    [Authorize]
    [HttpPost]
    public IActionResult Post(NewHobby hobby)
    {
        return Created("/", _repository.AddHobby(hobby.Name));
    }
    
    [Authorize]
    [HttpDelete]
    public IActionResult Delete(NewHobby hobby)
    {
        var status = _repository.DeleteHobby(hobby.Name);
        if (status == Status.NotFound)
            return NotFound();
        return Ok();
    }
    
    [Authorize]
    [HttpPost("/User/{id?}")]
    public IActionResult HobbyAdd(long id, AddHobby hobby)
    {
        var user = _repository.GetUser(id);
        if (user is null)
            return BadRequest();
        if (HttpContext.User.Identity.Name != user.Name)
            return StatusCode(403);
        if(_repository.GetHobby(hobby.Name) is null)
            return BadRequest();
        //_repository.UpdateUser(user, UserModificatedFields.HobbyAdd, hobby.Name);
        return Ok(_repository.UpdateUser(user, UserModificatedFields.HobbyAdd, hobby.Name));
    }
    
    [Authorize]
    [HttpDelete("/User/{id?}/{hobby?}")]
    public IActionResult HobbyRemove(long id, string hobby)
    {
        var user = _repository.GetUser(id);
        if (user is null)
            return BadRequest();
        if (HttpContext.User.Identity.Name != user.Name)
            return StatusCode(403);
        if(_repository.GetHobby(hobby) is null)
            return BadRequest();
        _repository.UpdateUser(user, UserModificatedFields.HobbyRemove, hobby);
        return Ok(_repository.UpdateUser(user, UserModificatedFields.HobbyRemove, hobby));
    }
}