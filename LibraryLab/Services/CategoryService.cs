using LibraryLab.Interfaces;
using LibraryLab.Models;

namespace LibraryLab.Services;

public class CategoryService : ICategoryService
{
    private readonly List<Category> _categories = new();
    public Category Create(string name)
    {
        var c = new Category { Name = name };
        _categories.Add(c);
        return c;
    }
    public IEnumerable<Category> GetAll() => _categories;
    public Category? Get(Guid id) => _categories.FirstOrDefault(c => c.Id == id);
    public void Update(Guid id, string name)
    {
        var c = Get(id) ?? throw new InvalidOperationException("Category not found");
        c.Name = name;
    }
    public void Delete(Guid id) => _categories.RemoveAll(c => c.Id == id);
}
