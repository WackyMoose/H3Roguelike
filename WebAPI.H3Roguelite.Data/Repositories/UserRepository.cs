using WebAPI.Data.Repositories;
using WebAPI.H3Roguelite.Data.Models;
using WebAPI.H3Roguelite.Data.Providers;

namespace WebAPI.H3Roguelite.Data.Repositories;

public interface IUserRepository : IRepository<User, int>
{
}

public class UserRepository : DbRepositoryBase<User, int>, IUserRepository
{
    public UserRepository(IH3RogueliteClientProvider provider) 
        : base(provider)
    {
    }
}
