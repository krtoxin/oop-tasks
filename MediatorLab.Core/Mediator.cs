using Microsoft.Extensions.DependencyInjection;

namespace MediatorLab.Core;

public sealed class Mediator : IMediator
{
    private readonly IServiceProvider _provider;
    public Mediator(IServiceProvider provider) => _provider = provider;

    public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
        var handler = _provider.GetRequiredService(handlerType);
        var method = handlerType.GetMethod("Handle")!;
        return (Task<TResponse>)method.Invoke(handler, new object?[] { request, cancellationToken })!;
    }

    public async Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification
    {
        var handlerType = typeof(INotificationHandler<>).MakeGenericType(typeof(TNotification));
        var handlers = _provider.GetServices(handlerType);
        foreach (var h in handlers)
        {
            var method = handlerType.GetMethod("Handle")!;
            var task = (Task)method.Invoke(h, new object?[] { notification, cancellationToken })!;
            await task.ConfigureAwait(false);
        }
    }
}

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMediator(this IServiceCollection services)
    {
        services.AddTransient<IMediator, Mediator>();
        services.AddTransient<ISender>(sp => sp.GetRequiredService<IMediator>());
        services.AddTransient<IPublisher>(sp => sp.GetRequiredService<IMediator>());
        return services;
    }
}
