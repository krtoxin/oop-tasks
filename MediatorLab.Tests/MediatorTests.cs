using MediatorLab.Core;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace MediatorLab.Tests;

public class MediatorTests
{
    private ServiceProvider BuildProvider()
    {
        var services = new ServiceCollection();
        services.AddMediator();
        services.AddTransient<IRequestHandler<PingQuery, string>, PingHandler>();
        services.AddTransient<IRequestHandler<EchoCommand, string>, EchoHandler>();
        services.AddTransient<INotificationHandler<UserRegistered>, WelcomeEmailHandler>();
        services.AddTransient<INotificationHandler<UserRegistered>, AuditLogHandler>();
        return services.BuildServiceProvider();
    }

    [Fact]
    public async Task Send_Query_Routes_To_Handler()
    {
        var sp = BuildProvider();
        var sender = sp.GetRequiredService<ISender>();
        var res = await sender.Send(new PingQuery("hello"));
        Assert.Equal("PONG: hello", res);
    }

    [Fact]
    public async Task Send_Command_Routes_To_Handler()
    {
        var sp = BuildProvider();
        var mediator = sp.GetRequiredService<IMediator>();
        var res = await mediator.Send(new EchoCommand("echo!"));
        Assert.Equal("echo!", res);
    }

    [Fact]
    public async Task Publish_Notifies_All_Handlers()
    {
        WelcomeEmailHandler.Received.Clear();
        AuditLogHandler.Logs.Clear();

        var sp = BuildProvider();
        var publisher = sp.GetRequiredService<IPublisher>();
        await publisher.Publish(new UserRegistered("user@example.com"));

        Assert.Contains("Welcome user@example.com", WelcomeEmailHandler.Received);
        Assert.Contains("Audit: user@example.com", AuditLogHandler.Logs);
    }
}
