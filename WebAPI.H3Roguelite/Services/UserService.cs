using WebAPI.H3Roguelite.Data.Models;
using WebAPI.H3Roguelite.Data.Repositories;
using WebAPI.Services;

namespace WebAPI.H3Roguelite.Services;

public interface IUserService : IService
{
    Task<IEnumerable<User?>> GetAsync();
    Task<User?> GetByIdAsync(int id);
    Task<bool> AddAsync(User user);
}

public class UserService : ServiceBase, IUserService
{
    public UserService(IUserRepository userRepository)
    {
        UserRepository = userRepository;
    }

    public IUserRepository UserRepository { get; }

    public Task<IEnumerable<User?>> GetAsync()
    {
        return UserRepository.GetAsync();
    }

    public Task<User?> GetByIdAsync(int id)
    {
        return UserRepository.GetAsync(id);
    }

    public Task<bool> AddAsync(User user)
    {
        return UserRepository.AddAsync(user);
    }
}
