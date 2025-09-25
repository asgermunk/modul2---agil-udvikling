public interface IAuthService
{
    LoginResponse Login(LoginRequest request);
}

public class AuthService : IAuthService
{
    private readonly IUserService _userService;

    public AuthService(IUserService userService)
    {
        _userService = userService;
    }

    public LoginResponse Login(LoginRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
        {
            return new LoginResponse
            {
                Success = false,
                Message = "Email and password are required."
            };
        }

        // Find user by email
        var user = _userService.GetAllUsers()
            .FirstOrDefault(u => u.Email?.Equals(request.Email, StringComparison.OrdinalIgnoreCase) == true);

        if (user == null)
        {
            return new LoginResponse
            {
                Success = false,
                Message = "Invalid email or password."
            };
        }

        // Check password (simple string comparison for dummy data)
        if (user.Password != request.Password)
        {
            return new LoginResponse
            {
                Success = false,
                Message = "Invalid email or password."
            };
        }

        // Login successful
        return new LoginResponse
        {
            Success = true,
            Message = "Login successful!",
            User = new User(user.Id, user.Name, user.Email, user.Password) // Don't return password
        };
    }
}