using LibraryLab.Models;

namespace LibraryLab.Interfaces;

public interface ISubscriptionService
{
    Subscription Subscribe(Guid userId, Guid categoryId);
    void Unsubscribe(Guid userId, Guid categoryId);
    IEnumerable<Subscription> GetByCategory(Guid categoryId);
    bool IsSubscribed(Guid userId, Guid categoryId);
}
