using LibraryLab.Models;

namespace LibraryLab.Interfaces;

public interface INotifier
{
    void NotifyNewBook(Book book, Category category);
}
