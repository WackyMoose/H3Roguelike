using WebAPI.H3Roguelite.Data.Models;
using WebAPI.H3Roguelite.Data.Repositories;
using WebAPI.H3Roguelite.Models;
using WebAPI.Services;

namespace WebAPI.H3Roguelite.Services;

public interface IAuthService : IService
{
    Task<AuthenticateResponse?> AuthenticateAsync(AuthenticateRequest authenticateRequest);
    Task<RegisterResponse?> RegisterAsync(RegisterRequest registerRequest);
}

internal class AuthService : ServiceBase, IAuthService
{
    public AuthService(IPasswordService passwordService, IUserRepository userRepository)
    {
        PasswordService = passwordService;
        UserRepository = userRepository;
    }

    public IPasswordService PasswordService { get; }
    protected IUserRepository UserRepository { get; }

    public async Task<AuthenticateResponse?> AuthenticateAsync(AuthenticateRequest authenticateRequest)
    {
        Throw.IfNull(authenticateRequest, "AuthenticateRequest is null");
        Throw.IfStringIsNullOrWhiteSpace(authenticateRequest.Username, "Username is null");
        Throw.IfStringIsNullOrWhiteSpace(authenticateRequest.Password, "Password is null");

        var user = await UserRepository.GetUserByUsernameAsync(authenticateRequest.Username!);
        if (user == default)
        {
            return default;
        }

        if(!PasswordService.VerifyPassword(user!, user!.Password, authenticateRequest.Password))
        {
            return default;
        }

        return new AuthenticateResponse
        {
            Username = user.Username
        };
    }

    public async Task<RegisterResponse?> RegisterAsync(RegisterRequest registerRequest)
    {
        Throw.IfNull(registerRequest, "AuthenticateRequest is null");
        Throw.IfStringIsNullOrWhiteSpace(registerRequest.Username, "Username is null");
        Throw.IfStringIsNullOrWhiteSpace(registerRequest.Password, "Password is null");

        var user = await UserRepository.GetUserByUsernameAsync(registerRequest.Username!);
        if(user != default)
        {
            return default;
        }

        var newUser = new User
        {
            Username = registerRequest.Username
        };

        newUser.Password = PasswordService.HashPassword(newUser, registerRequest.Password);

        var inserted = await UserRepository.AddAsync(newUser);

        return new RegisterResponse
        {
        };
    }
}
