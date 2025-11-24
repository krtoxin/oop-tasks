namespace LibraryLab.Models;

public class Subscription
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public Guid CategoryId { get; set; }
}
