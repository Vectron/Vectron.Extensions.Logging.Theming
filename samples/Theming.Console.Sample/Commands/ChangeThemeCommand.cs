using Microsoft.Extensions.Configuration;
using Vectron.Extensions.Logging.Theming;
using Vectron.InteractiveConsole.Commands;

namespace Theming.Console.Sample.Commands;

/// <summary>
/// An <see cref="IConsoleCommand"/> for changing the logging theme.
/// </summary>
/// <param name="configuration">The configuration to change.</param>
/// <param name="loggingThemes">A collection of valid themes.</param>
internal sealed class ChangeThemeCommand(IConfiguration configuration, IEnumerable<ITheme> loggingThemes) : IConsoleCommand
{
    /// <summary>
    /// The parameter needed for this command.
    /// </summary>
    public const string Parameter = "theme";

    private readonly string[] validThemes = loggingThemes.Select(x => x.Name).ToArray();

    /// <inheritdoc/>
    public string[]? ArgumentNames => ["ThemeName"];

    /// <inheritdoc/>
    public string[] CommandParameters => [Parameter];

    /// <inheritdoc/>
    public string HelpText => "Change the logging theme";

    /// <inheritdoc/>
    public int MaxArguments => 1;

    /// <inheritdoc/>
    public int MinArguments => 0;

    /// <inheritdoc/>
    public void Execute(string[] arguments)
    {
        if (arguments.Length == 0)
        {
            var currentTheme = configuration["Logging:Console:FormatterOptions:Theme"];
            System.Console.WriteLine($"Current theme: {currentTheme}");
            return;
        }

        var themeName = arguments[0];
        var foundTheme = validThemes.FirstOrDefault(x => x.Equals(themeName, StringComparison.OrdinalIgnoreCase));

        if (foundTheme == null)
        {
            System.Console.WriteLine($"{themeName} is not a valid theme");
            return;
        }

        configuration["Logging:Console:FormatterOptions:Theme"] = foundTheme;
        System.Console.WriteLine($"Theme changed to {foundTheme}");
    }
}
