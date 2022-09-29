using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using WebAPI.H3Roguelite.Data.Models;
using WebAPI.Services;

namespace WebAPI.H3Roguelite.Services;

public interface IPasswordService : IService
{
    string HashPassword(User user, string? password);
    bool VerifyPassword(User user, string? hashedPassword, string? password);
}

internal class PasswordService : ServiceBase, IPasswordService
{
    private readonly IPasswordHasher<User> _passwordHasher;

    public PasswordService()
    {
        var options = Options.Create(new PasswordHasherOptions
        {
            IterationCount = 10000,
            CompatibilityMode = PasswordHasherCompatibilityMode.IdentityV3
        });

        _passwordHasher = new PasswordHasher<User>(options);
    }

    public string HashPassword(User user, string? password)
    {
        return _passwordHasher.HashPassword(user, password);
    }

    public bool VerifyPassword(User user, string? hashedPassword, string? password)
    {
        var verifyResult = _passwordHasher.VerifyHashedPassword(user, hashedPassword, password);
        if (verifyResult == PasswordVerificationResult.SuccessRehashNeeded)
        {
            throw new Exception("Password needs rehashing!!!");
        }
        return verifyResult != PasswordVerificationResult.Failed;
    }
}
