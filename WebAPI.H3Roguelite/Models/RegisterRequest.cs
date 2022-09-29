using System.ComponentModel.DataAnnotations;

namespace WebAPI.H3Roguelite.Models;

public class RegisterRequest
{
    [Required]
    public string? Username { get; set; }

    [Required]
    public string? Password { get; set; }
}