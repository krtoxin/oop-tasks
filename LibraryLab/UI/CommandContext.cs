using LibraryLab.Interfaces;

namespace LibraryLab.UI;

internal sealed class CommandContext
{
    public IUserService Users { get; }
    public ICategoryService Categories { get; }
    public IBookService Books { get; }
    public ISubscriptionService Subscriptions { get; }
    public INotifier Notifier { get; }

    public CommandContext(IUserService users, ICategoryService categories, IBookService books, ISubscriptionService subs, INotifier notifier)
    {
        Users = users;
        Categories = categories;
        Books = books;
        Subscriptions = subs;
        Notifier = notifier;
    }

    public string Prompt(string label)
    {
        Console.Write(label);
        return Console.ReadLine() ?? string.Empty;
    }

    public Guid PromptGuid(string label)
    {
        Console.Write(label);
        return Guid.TryParse(Console.ReadLine(), out var g) ? g : Guid.Empty;
    }
}
