namespace modul2_agiludvikling.Services;
public interface IUserService
{
    IEnumerable<User> GetAllUsers();
    User? GetUserById(int id);
    User CreateUser(User user);
    bool UpdateUser(int id, User user);
    bool DeleteUser(int id);
}