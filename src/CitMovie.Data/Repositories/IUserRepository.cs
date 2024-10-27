using CitMovie.Models.DomainObjects;

namespace CitMovie.Data;

public interface IUserRepository
{
    Task<User> GetUserAsync(string username);
    Task<User> GetUserAsync(int id);
    Task<User> CreateUserAsync(User user);
    Task<User> UpdateUserAsync(User user);
    Task<bool> DeleteUserAsync(int id);
}