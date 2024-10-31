namespace CitMovie.Business;

public class UserManager : IUserManager
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserManager(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserResult> CreateUserAsync(UserCreateRequest userRequest)
    {
        User newUser = _mapper.Map<User>(userRequest);
        User result = await _userRepository.CreateUserAsync(newUser);

        return _mapper.Map<UserResult>(result);
    }

    public async Task<bool> DeleteUserAsync(int id)
        => await _userRepository.DeleteUserAsync(id);

    public async Task<UserResult> GetUserAsync(string username) {
        User result = await _userRepository.GetUserAsync(username);
        return _mapper.Map<UserResult>(result);
    }

    public async Task<UserResult> GetUserAsync(int id)
    {
        User result = await _userRepository.GetUserAsync(id);
        return _mapper.Map<UserResult>(result);
    }

    public async Task<UserResult> UpdateUserAsync(int id, UserUpdateRequest userRequest)
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
        return _mapper.Map<UserResult>(result);
    }
}