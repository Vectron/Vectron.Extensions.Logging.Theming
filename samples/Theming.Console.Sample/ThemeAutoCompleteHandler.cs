using Theming.Console.Sample.Commands;
using Vectron.Extensions.Logging.Theming;
using Vectron.InteractiveConsole.AutoComplete;

namespace Theming.Console.Sample;

/// <summary>
/// An <see cref="IAutoCompleteHandler"/> for suggesting theme names.
/// </summary>
/// <param name="loggingThemes">The installed themes.</param>
internal sealed class ThemeAutoCompleteHandler(IEnumerable<ITheme> loggingThemes) : IAutoCompleteHandler
{
    private const char ArgumentSeparator = ' ';
    private readonly string[] validThemes = loggingThemes.Select(x => x.Name).ToArray();
    private LinkedList<string> autoCompletions = new();
    private LinkedListNode<string>? current;
    private string rootCommand = string.Empty;

    /// <inheritdoc/>
    public string? NextSuggestions(string text)
    {
        if (!CanHandle(text))
        {
            return null;
        }

        Init(text);
        if (current == null)
        {
            current = autoCompletions.First;
            return current?.Value;
        }

        current = current.Next;
        return current?.Value;
    }

    /// <inheritdoc/>
    public string? PreviousSuggestions(string text)
    {
        if (!CanHandle(text))
        {
            return null;
        }

        Init(text);
        if (current == null)
        {
            current = autoCompletions.Last;
            return current?.Value;
        }

        current = current.Previous;
        return current?.Value;
    }

    private static bool CanHandle(string text)
    {
        var parameters = text.Split(ArgumentSeparator);
        if (parameters.Length is > 3 or < 2)
        {
            return false;
        }

        if (!string.Equals(parameters[0], ChangeThemeCommand.Parameter, StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        return true;
    }

    private void Init(string text)
    {
        if (rootCommand.Equals(text, StringComparison.OrdinalIgnoreCase)
            && autoCompletions.Count > 0)
        {
            return;
        }

        rootCommand = text;
        var arguments = rootCommand.Split(ArgumentSeparator);
        current = null;
        autoCompletions.Clear();

        var preparedText = arguments.Length > 2 ? arguments[2] : string.Empty;
        var validCommandOptions = validThemes
            .Where(x => x.StartsWith(preparedText, StringComparison.OrdinalIgnoreCase))
            .Select(x => $"{arguments[0]} {x}");

        autoCompletions = new LinkedList<string>(validCommandOptions);
    }
}
