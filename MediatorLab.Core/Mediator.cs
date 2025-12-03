using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MediatorLab.Core;

public sealed class Mediator : IMediator
{
    private readonly IServiceProvider _provider;
    private readonly Dictionary<Type, MethodInfo> _sendCache = new();
    public Mediator(IServiceProvider provider) => _provider = provider;

    public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        var reqType = request.GetType();
        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(reqType, typeof(TResponse));
        var handler = _provider.GetService(handlerType);
        if (handler is null)
            throw new InvalidOperationException($"No handler registered for request type '{reqType.FullName}'.");

        if (!_sendCache.TryGetValue(reqType, out var method))
        {
            method = handlerType.GetMethod("Handle")!;
            _sendCache[reqType] = method;
        }
        return (Task<TResponse>)method.Invoke(handler, new object?[] { request, cancellationToken })!;
    }

    public async Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification
    {
        var handlerType = typeof(INotificationHandler<>).MakeGenericType(typeof(TNotification));
        var handlers = _provider.GetServices(handlerType).ToList();
        var exceptions = new List<Exception>();
        foreach (var h in handlers)
        {
            var method = handlerType.GetMethod("Handle")!;
            try
            {
                var task = (Task)method.Invoke(h, new object?[] { notification, cancellationToken })!;
                await task.ConfigureAwait(false);
            }
            catch (TargetInvocationException tie) when (tie.InnerException != null)
            {
                exceptions.Add(tie.InnerException);
            }
            catch (Exception ex)
            {
                exceptions.Add(ex);
            }
        }
        if (exceptions.Count == 1) throw exceptions[0];
        if (exceptions.Count > 1) throw new AggregateException(exceptions);
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
