using LibraryLab.Interfaces;
using LibraryLab.Models;

namespace LibraryLab.Services;

public class UserService : IUserService
{
    private readonly List<User> _users = new();
    public User Create(string name, string email)
    {
        var u = new User { Name = name, Email = email };
        _users.Add(u);
        return u;
    }
    public IEnumerable<User> GetAll() => _users;
    public User? Get(Guid id) => _users.FirstOrDefault(u => u.Id == id);
    public void Update(Guid id, string name, string email)
    {
        var u = Get(id) ?? throw new InvalidOperationException("User not found");
        u.Name = name; u.Email = email;
    }
    public void Delete(Guid id) => _users.RemoveAll(u => u.Id == id);
}
