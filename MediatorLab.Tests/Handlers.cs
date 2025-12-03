using MediatorLab.Core;

namespace MediatorLab.Tests;

public sealed record PingQuery(string Message) : IQuery<string>;
public sealed class PingHandler : IRequestHandler<PingQuery, string>
{
    public Task<string> Handle(PingQuery request, CancellationToken cancellationToken) => Task.FromResult($"PONG: {request.Message}");
}

public sealed record EchoCommand(string Text) : ICommand<string>;
public sealed class EchoHandler : IRequestHandler<EchoCommand, string>
{
    public Task<string> Handle(EchoCommand request, CancellationToken cancellationToken) => Task.FromResult(request.Text);
}

public sealed record UserRegistered(string Email) : INotification;
public sealed class WelcomeEmailHandler : INotificationHandler<UserRegistered>
{
    public Task Handle(UserRegistered notification, CancellationToken cancellationToken)
    {
        Received.Add($"Welcome {notification.Email}");
        return Task.CompletedTask;
    }
    public static List<string> Received { get; } = new();
}

public sealed class AuditLogHandler : INotificationHandler<UserRegistered>
{
    public Task Handle(UserRegistered notification, CancellationToken cancellationToken)
    {
        Logs.Add($"Audit: {notification.Email}");
        return Task.CompletedTask;
    }
    public static List<string> Logs { get; } = new();
}
