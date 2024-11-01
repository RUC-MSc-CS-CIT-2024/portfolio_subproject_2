namespace CitMovie.Business;

public interface IUserManager
{
    Task<UserResult> GetUserAsync(string username);
    Task<UserResult> GetUserAsync(int id);
    Task<UserResult> CreateUserAsync(UserCreateRequest user);
    Task<UserResult> UpdateUserAsync(int id, UserUpdateRequest user);
    Task<bool> DeleteUserAsync(int id);
}