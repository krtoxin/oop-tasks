using LibraryLab.Interfaces;
using LibraryLab.Models;

namespace LibraryLab.Services;

public class BookService : IBookService
{
    private readonly List<Book> _books = new();
    public Book Create(string title, string author, Guid categoryId)
    {
        var b = new Book { Title = title, Author = author, CategoryId = categoryId };
        _books.Add(b);
        return b;
    }
    public IEnumerable<Book> GetAll() => _books;
    public Book? Get(Guid id) => _books.FirstOrDefault(b => b.Id == id);
    public void Update(Guid id, string title, string author, Guid categoryId)
    {
        var b = Get(id) ?? throw new InvalidOperationException("Book not found");
        b.Title = title; b.Author = author; b.CategoryId = categoryId;
    }
    public void Delete(Guid id) => _books.RemoveAll(b => b.Id == id);
}
