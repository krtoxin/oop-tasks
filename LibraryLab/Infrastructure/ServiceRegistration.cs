using LibraryLab.Interfaces;
using LibraryLab.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryLab.Infrastructure;

public static class ServiceRegistration
{
    public static IServiceCollection AddLibraryServices(this IServiceCollection services)
    {
        services.AddSingleton<IUserService, UserService>();
        services.AddSingleton<ICategoryService, CategoryService>();
        services.AddSingleton<IBookService, BookService>();
        services.AddSingleton<ISubscriptionService, SubscriptionService>();
        services.AddSingleton<IEmailSender, ConsoleEmailSender>();
        services.AddSingleton<INotifier, Notifier>();
        return services;
    }
}
