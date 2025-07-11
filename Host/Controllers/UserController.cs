using Api.Hosting;
using Api.Requests;
using Application;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route(Routes.User)]
public class UserController(IAmAUserService UserService) : ControllerBase
{
    [HttpGet]
    public async Task<IList<User>> GetUsers()
    {
        return await UserService.GetAll();
    }    
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<User>> GetUser(Guid id)
    {
        var user = await UserService.Get(id);
        if (user is null) return NotFound();
        return user;
    }    
    
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateUser([FromBody] UserPayload payload)
    {
        var id = await UserService.Add(payload.Name, payload.Email);
        return Created($"/{Routes.User}/{id}", id);
    }    
    
    [HttpPut("{id:guid}")]
    public async Task UpdateUser(Guid id, [FromBody] UserPayload payload)
    {
        await UserService.Update(id, payload.Name, payload.Email);
    }    
    
    [HttpDelete("{id:guid}")]
    public async Task DeleteUser(Guid id)
    {
        await UserService.Remove(id);
    }
}