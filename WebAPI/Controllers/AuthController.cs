using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.H3Roguelite.Models;
using WebAPI.H3Roguelite.Services;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    public AuthController(IAuthService authService)
    {
        AuthService = authService;
    }

    protected IAuthService AuthService { get; }

    [HttpPost]
    [Route("authenticate", Name = nameof(AuthenticateAsync))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AuthenticateAsync(AuthenticateRequest authenticateRequest)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var authenticateResponse = await AuthService.AuthenticateAsync(authenticateRequest);
            if(authenticateResponse == default)
            {
                return NotFound();
            }

            return Ok(authenticateResponse);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }
}
