namespace LibraryLab.UI;

internal sealed class MenuHandler
{
    private readonly List<Command> _commands;
    private readonly CommandContext _ctx;
    public bool ExitRequested { get; private set; }

    public MenuHandler(List<Command> commands, CommandContext ctx)
    {
        _commands = commands;
        _ctx = ctx;
    }

    public void Show()
    {
        Console.WriteLine("\n==== Library Menu ====");
        int i = 1;
        foreach (var c in _commands)
            Console.WriteLine($"{i++}. {c.Key,-15} {c.Description}");
        Console.WriteLine("Type 'help' to reprint list");
    }

    public bool TryExecute(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return false;
        Command? cmd = null;
        if (int.TryParse(input, out var idx) && idx >= 1 && idx <= _commands.Count)
            cmd = _commands[idx - 1];
        cmd ??= _commands.FirstOrDefault(c => c.Matches(input));
        if (cmd == null) return false;
        if (cmd.Key == "exit") { ExitRequested = true; return true; }
        cmd.Execute(_ctx);
        return true;
    }
}
