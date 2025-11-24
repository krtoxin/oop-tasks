using LibraryLab.Models;

namespace LibraryLab.Interfaces;

public interface IBookService
{
    Book Create(string title, string author, Guid categoryId);
    IEnumerable<Book> GetAll();
    Book? Get(Guid id);
    void Update(Guid id, string title, string author, Guid categoryId);
    void Delete(Guid id);
}
