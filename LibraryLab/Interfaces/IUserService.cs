using LibraryLab.Models;

namespace LibraryLab.Interfaces;

public interface IUserService
{
    User Create(string name, string email);
    IEnumerable<User> GetAll();
    User? Get(Guid id);
    void Update(Guid id, string name, string email);
    void Delete(Guid id);
}
