public interface IUserService
{
    IEnumerable<User> GetAllUsers();
    User? GetUserById(int id);
    User CreateUser(User user);
    bool UpdateUser(int id, User user);
    bool DeleteUser(int id);
}

public class UserService : IUserService
{
    private readonly List<User> _users;

    public UserService()
    {
        _users = new List<User>
        {
            new User(1, "Alice", "alice@example.com", "password123"),
            new User(2, "Bob", "bob@example.com", "password456")
        };
    }

    public IEnumerable<User> GetAllUsers()
    {
        return _users;
    }

    public User? GetUserById(int id)
    {
        return _users.FirstOrDefault(u => u.Id == id);
    }

    public User CreateUser(User user)
    {
        var nextId = _users.Any() ? _users.Max(u => u.Id ?? 0) + 1 : 1;
        user.Id = nextId;
        _users.Add(user);
        return user;
    }

    public bool UpdateUser(int id, User user)
    {
        var existingUser = _users.FirstOrDefault(u => u.Id == id);
        if (existingUser == null)
            return false;

        existingUser.Name = user.Name;
        existingUser.Email = user.Email;
        existingUser.Password = user.Password;
        return true;
    }

    public bool DeleteUser(int id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user == null)
            return false;

        _users.Remove(user);
        return true;
    }
}