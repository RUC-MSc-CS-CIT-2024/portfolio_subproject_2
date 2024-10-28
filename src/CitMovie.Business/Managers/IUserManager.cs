namespace CitMovie.Business;

public interface IUserManager
{
    Task<UserResponse> GetUserAsync(string username);
    Task<UserResponse> GetUserAsync(int id);
    Task<UserResponse> CreateUserAsync(UserCreateRequest user);
    Task<UserResponse> UpdateUserAsync(int id, UserUpdateRequest user);
    Task<bool> DeleteUserAsync(int id);
}