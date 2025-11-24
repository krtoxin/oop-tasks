using LibraryLab.Interfaces;
using LibraryLab.Models;

namespace LibraryLab.Services;

public class Notifier : INotifier
{
    private readonly ISubscriptionService _subs;
    private readonly IUserService _users;
    private readonly ICategoryService _categories;
    private readonly IEmailSender _email;
    public Notifier(ISubscriptionService subs, IUserService users, ICategoryService categories, IEmailSender email)
    {
        _subs = subs; _users = users; _categories = categories; _email = email;
    }
    public void NotifyNewBook(Book book, Category category)
    {
        foreach (var sub in _subs.GetByCategory(category.Id))
        {
            var user = _users.Get(sub.UserId);
            if (user != null)
            {
                _email.Send(user.Email, $"New book in category {category.Name}", $"{book.Title} - {book.Author}");
            }
        }
    }
}
