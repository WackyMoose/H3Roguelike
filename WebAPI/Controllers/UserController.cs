using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

public class User
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
	public UserController()
	{
	}

    [HttpGet]
    public Task<User> GetByIdAsync(int id)
    {
        return Task.FromResult(new User
        {
            Id = id,
            Name = "Kurt",
            Created = DateTime.Now,
            Updated = DateTime.Now
        });
    }

    [HttpPost]
    public Task<User> AddUser(User user)
    {
        return Task.FromResult(user);
    }
}
