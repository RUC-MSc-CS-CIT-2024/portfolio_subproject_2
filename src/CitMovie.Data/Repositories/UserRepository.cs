namespace CitMovie.Data;

public class UserRepository : IUserRepository
{
    private readonly FrameworkContext _context;

    public UserRepository(FrameworkContext context)
    {
        _context = context;
    }

    public async Task<User> CreateUserAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<bool> DeleteUserAsync(int id)
    {   
        try {
            User userToDelete = _context.Users.First(x => x.Id == id);
            _context.Users.Remove(userToDelete);
            await _context.SaveChangesAsync();
            return true;
        } catch {
            return false;
        }
    }

    public Task<User> GetUserAsync(string username)
        => _context.Users.FirstAsync(x => x.Username == username);

    public Task<User> GetUserAsync(int id)
        => _context.Users.FirstAsync(x => x.Id == id);

    public async Task<User> UpdateUserAsync(User user)
    {
        User existingUser = await _context.Users.FirstAsync(x => x.Id == user.Id);
        _context.Entry(existingUser).CurrentValues.SetValues(user);
        
        await _context.SaveChangesAsync();
        return existingUser;
    }
}