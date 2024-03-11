using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SampleHelpers;
using Vectron.Ansi;
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
            configuration["Logging:Console:FormatterOptions:IncludeScopes"] = "false";
            configuration["Logging:Console:FormatterOptions:ColorWholeLine"] = "false";
            System.Console.Out.WriteLine();
            System.Console.Out.WriteColored($"Theme: {theme}, IncludeScopes: false, ColorWholeLine: false" + Environment.NewLine, AnsiColor.Cyan);
            logger.WriteAll();

            configuration["Logging:Console:FormatterOptions:IncludeScopes"] = "true";
            configuration["Logging:Console:FormatterOptions:ColorWholeLine"] = "false";
            System.Console.Out.WriteLine();
            System.Console.Out.WriteColored($"Theme: {theme}, IncludeScopes: true, ColorWholeLine: false" + Environment.NewLine, AnsiColor.Cyan);
            logger.WriteAll();

            configuration["Logging:Console:FormatterOptions:IncludeScopes"] = "false";
            configuration["Logging:Console:FormatterOptions:ColorWholeLine"] = "true";
            System.Console.Out.WriteLine();
            System.Console.Out.WriteColored($"Theme: {theme}, IncludeScopes: false, ColorWholeLine: true" + Environment.NewLine, AnsiColor.Cyan);
            logger.WriteAll();

            configuration["Logging:Console:FormatterOptions:IncludeScopes"] = "false";
            configuration["Logging:Console:FormatterOptions:ColorWholeLine"] = "true";
            System.Console.Out.WriteLine();
            System.Console.Out.WriteColored($"Theme: {theme}, IncludeScopes: true, ColorWholeLine: true" + Environment.NewLine, AnsiColor.Cyan);
            logger.WriteAll();
        }
    }
}
