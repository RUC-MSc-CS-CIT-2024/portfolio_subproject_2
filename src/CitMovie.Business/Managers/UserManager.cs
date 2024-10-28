namespace CitMovie.Business;

public class UserManager : IUserManager
{
    private readonly IUserRepository _userRepository;

    public UserManager(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserResponse> CreateUserAsync(UserCreateRequest userRequest)
    {
        User newUser = new User
        {
            Username = userRequest.Username,
            Email = userRequest.Email,
            Password = userRequest.Password
        };
        User result = await _userRepository.CreateUserAsync(newUser);

        return new UserResponse(result.Id, result.Username, result.Email);
    }

    public async Task<bool> DeleteUserAsync(int id)
        => await _userRepository.DeleteUserAsync(id);

    public async Task<UserResponse> GetUserAsync(string username) {
        User result = await _userRepository.GetUserAsync(username);
        return new UserResponse(result.Id, result.Username, result.Email);
    }

    public async Task<UserResponse> GetUserAsync(int id)
    {
        User result = await _userRepository.GetUserAsync(id);
        return new UserResponse(result.Id, result.Username, result.Email);
    }

    public async Task<UserResponse> UpdateUserAsync(int id, UserUpdateRequest userRequest)
    {
        User existingUser = await _userRepository.GetUserAsync(id);

        User updatedUser = new User
        {
            Id = id,
            Username = userRequest.Username ?? existingUser.Username,
            Email = userRequest.Email ?? existingUser.Email,
            Password = userRequest.Password ?? existingUser.Password
        };

        User result = await _userRepository.UpdateUserAsync(updatedUser);
        return new UserResponse(result.Id, result.Username, result.Email);
    }
}