namespace CitMovie.Business;

public class UserManager : IUserManager
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IPasswordHelper _passwordHelper;

    public UserManager(IUserRepository userRepository, IMapper mapper, IPasswordHelper passwordHelper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _passwordHelper = passwordHelper;
    }

    public async Task<UserResult> CreateUserAsync(UserCreateRequest userRequest)
    {
        User newUser = _mapper.Map<User>(userRequest);
        newUser.Salt = _passwordHelper.GenerateSalt();
        newUser.HashedPassword = _passwordHelper.HashPassword(userRequest.Password, newUser.Salt);

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

        existingUser.Username = userRequest.Username ?? existingUser.Username;
        existingUser.Email = userRequest.Email ?? existingUser.Email;
        if (userRequest.Password != null)
        {
            existingUser.Salt = _passwordHelper.GenerateSalt();
            existingUser.HashedPassword = _passwordHelper.HashPassword(userRequest.Password, existingUser.Salt);
        }

        User result = await _userRepository.UpdateUserAsync(existingUser);
        return _mapper.Map<UserResult>(result);
    }
}