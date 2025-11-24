namespace LibraryLab.UI;

internal sealed class Command
{
    public string Key { get; }
    public string Description { get; }
    public string[] Aliases { get; }
    private readonly Action<CommandContext> _action;

    public Command(string key, string description, Action<CommandContext> action, params string[] aliases)
    {
        Key = key;
        Description = description;
        _action = action;
        Aliases = aliases ?? Array.Empty<string>();
    }

    public bool Matches(string input) =>
        Key.Equals(input, StringComparison.OrdinalIgnoreCase) ||
        Aliases.Any(a => a.Equals(input, StringComparison.OrdinalIgnoreCase));

    public void Execute(CommandContext ctx) => _action(ctx);
}
