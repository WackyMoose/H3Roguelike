using System.ComponentModel.DataAnnotations;

namespace WebAPI.H3Roguelite.Models;

public class AuthenticateRequest
{
    [Required]
    public string? Username { get; set; }

    [Required]
    public string? Password { get; set; }
}
