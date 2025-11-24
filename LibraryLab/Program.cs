using Microsoft.Extensions.DependencyInjection;
using LibraryLab.Infrastructure;
using LibraryLab.Interfaces;
using LibraryLab.UI;

namespace LibraryLab;

internal static class Program
{
    static void Main()
    {
        var services = new ServiceCollection()
            .AddLibraryServices()
            .BuildServiceProvider();

        var ctx = new CommandContext(
            services.GetRequiredService<IUserService>(),
            services.GetRequiredService<ICategoryService>(),
            services.GetRequiredService<IBookService>(),
            services.GetRequiredService<ISubscriptionService>(),
            services.GetRequiredService<INotifier>());

        SeedDemoData(ctx);

        var commands = MenuBuilder.Build(ctx);
        var menu = new MenuHandler(commands, ctx);

        while (!menu.ExitRequested)
        {
            menu.Show();
            Console.Write("Command (key or number): ");
            var input = (Console.ReadLine() ?? string.Empty).Trim();
            if (!menu.TryExecute(input))
                Console.WriteLine("Unknown command. Type 'help'.");
        }

        Console.WriteLine("Goodbye");
    }

    private static void SeedDemoData(CommandContext ctx)
    {
        var u1 = ctx.Users.Create("Alice", "alice@example.com");
        var u2 = ctx.Users.Create("Bob", "bob@example.com");
        var catSci = ctx.Categories.Create("Science");
        var catArt = ctx.Categories.Create("Art");
        ctx.Subscriptions.Subscribe(u1.Id, catSci.Id);
        ctx.Subscriptions.Subscribe(u2.Id, catArt.Id);
    }
}
