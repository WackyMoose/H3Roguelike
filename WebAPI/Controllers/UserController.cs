using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.H3Roguelite.Data.Models;
using WebAPI.H3Roguelite.Services;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    public UserController(IUserService userService)
	{
        UserService = userService;
    }

    public IUserService UserService { get; }

    [HttpGet]
    [Route("", Name = nameof(GetUsersAsync))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetUsersAsync()
    {
        var users = await UserService.GetAsync();
        
        return Ok(users);
    }

    [HttpGet]
    [Route("{id}", Name = nameof(GetUserByIdAsync))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetUserByIdAsync(int id)
    {
        var user = await UserService.GetByIdAsync(id);

        return Ok(user);
    }

    [HttpPost]
    [Route("", Name = nameof(AddUserAsync))]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddUserAsync(User user)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var created = await UserService.AddAsync(user);
            if(created)
            {
                return CreatedAtRoute(nameof(GetUserByIdAsync), new { id = user.Id }, user);
            }

            return BadRequest();
        }
        catch(Exception ex)
        {
            return BadRequest(ex);
        }
    }
}
