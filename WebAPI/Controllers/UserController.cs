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
        try
        {
            var users = await UserService.GetAsync();
            if (users == default)
            {
                return NotFound();
            }

            return Ok(users);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpGet]
    [Route("{id}", Name = nameof(GetUserByIdAsync))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetUserByIdAsync(int id)
    {
        try
        {
            var user = await UserService.GetByIdAsync(id);
            if (user == default)
            {
                return NotFound();
            }

            return Ok(user);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpPost]
    [Route("", Name = nameof(AddUserAsync))]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddUserAsync(User user)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var created = await UserService.AddAsync(user);
            if (created)
            {
                return CreatedAtRoute(nameof(GetUserByIdAsync), new { id = user.Id }, user);
            }

            return BadRequest();
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }
}
