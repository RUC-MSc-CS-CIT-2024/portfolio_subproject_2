using CitMovie.Models.DomainObjects;
using Microsoft.EntityFrameworkCore;

namespace CitMovie.Data;

public class UserRepository : IUserRepository
{
    private readonly FrameworkContext _context;

    public UserRepository(FrameworkContext context)
    {
        _context = context;
    }

    public Task<User> CreateUserAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteUserAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetUserAsync(string username)
        => _context.Users.FirstAsync(x => x.Username == username);

    public Task<User> GetUserAsync(int id)
        => _context.Users.FirstAsync(x => x.Id == id);

    public Task<User> UpdateUserAsync(User user)
    {
        throw new NotImplementedException();
    }
}