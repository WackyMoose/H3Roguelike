using WebAPI.Data.Repositories;
using WebAPI.H3Roguelite.Data.Models;
using WebAPI.H3Roguelite.Data.Providers;

namespace WebAPI.H3Roguelite.Data.Repositories;

public interface IUserRepository : IRepository<User, int>
{
    Task<User?> GetUserByUsernameAsync(string username);
}

public class UserRepository : DbRepositoryBase<User, int>, IUserRepository
{
    public UserRepository(IH3RogueliteClientProvider provider) 
        : base(provider)
    {
    }

    public Task<User?> GetUserByUsernameAsync(string username)
    {
        var sql = "SELECT * FROM `User` WHERE `Username` = @username LIMIT 1";
        return Provider.QueryFirstOrDefaultAsync<User>(sql, new { username });
    }
}
