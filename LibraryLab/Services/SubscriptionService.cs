using LibraryLab.Interfaces;
using LibraryLab.Models;

namespace LibraryLab.Services;

public class SubscriptionService : ISubscriptionService
{
    private readonly List<Subscription> _subs = new();
    public Subscription Subscribe(Guid userId, Guid categoryId)
    {
        if (IsSubscribed(userId, categoryId)) throw new InvalidOperationException("Already subscribed");
        var s = new Subscription { UserId = userId, CategoryId = categoryId };
        _subs.Add(s);
        return s;
    }
    public void Unsubscribe(Guid userId, Guid categoryId) => _subs.RemoveAll(s => s.UserId == userId && s.CategoryId == categoryId);
    public IEnumerable<Subscription> GetByCategory(Guid categoryId) => _subs.Where(s => s.CategoryId == categoryId);
    public bool IsSubscribed(Guid userId, Guid categoryId) => _subs.Any(s => s.UserId == userId && s.CategoryId == categoryId);
}
