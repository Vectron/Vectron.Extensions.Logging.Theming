using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SampleHelpers;
using Vectron.Extensions.Logging.Theming;
using Vectron.InteractiveConsole.Commands;

namespace Theming.Console.Sample.Commands;

/// <summary>
/// Show the output of all available themes.
/// </summary>
internal sealed class ShowAllCommand(IConfiguration configuration, IEnumerable<ITheme> loggingThemes, ILogger<ShowAllCommand> logger) : IConsoleCommand
{
    private readonly string[] validThemes = loggingThemes.Select(x => x.Name).ToArray();

    /// <inheritdoc/>
    public string[]? ArgumentNames => null;

    /// <inheritdoc/>
    public string[] CommandParameters => ["demo"];

    /// <inheritdoc/>
    public string HelpText => "Cycle through all themes and write log messages";

    /// <inheritdoc/>
    public int MaxArguments => 0;

    /// <inheritdoc/>
    public int MinArguments => 0;

    /// <inheritdoc/>
    public void Execute(string[] arguments)
    {
        foreach (var theme in validThemes)
        {
            configuration["Logging:Console:FormatterOptions:Theme"] = theme;
            System.Console.WriteLine($"Theme changed to {theme}");
            logger.WriteAll();
        }
    }
}
