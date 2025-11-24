using LibraryLab.Models;

namespace LibraryLab.Interfaces;

public interface ICategoryService
{
    Category Create(string name);
    IEnumerable<Category> GetAll();
    Category? Get(Guid id);
    void Update(Guid id, string name);
    void Delete(Guid id);
}
