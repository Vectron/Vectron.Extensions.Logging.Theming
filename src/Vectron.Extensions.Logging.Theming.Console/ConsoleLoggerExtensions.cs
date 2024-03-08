using System.Runtime.Versioning;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace Vectron.Extensions.Logging.Theming.Console;

/// <summary>
/// Extensions for setting up the logging builder.
/// </summary>
[UnsupportedOSPlatform("browser")]
public static class ConsoleLoggerExtensions
{
    /// <summary>
    /// Add the console log formatter named 'SingleLineFormatter' to the factory with default properties.
    /// </summary>
    /// <param name="builder">The <see cref="ILoggingBuilder"/> to use.</param>
    /// <returns>The <see cref="ILoggingBuilder"/> for chaining.</returns>
    public static ILoggingBuilder AddSingleLineThemedConsole(this ILoggingBuilder builder)
        => builder
            .AddConsole(options => options.FormatterName = SingleLineThemedConsoleFormatter.FormatterName)
            .AddSingleLineThemedConsoleFormatter();

    /// <summary>
    /// Add and configure a console log formatter named 'SingleLineFormatter' to the factory.
    /// </summary>
    /// <param name="builder">The <see cref="ILoggingBuilder"/> to use.</param>
    /// <param name="configure">
    /// A delegate to configure the <see cref="ConsoleLogger"/> options for the built-in default log formatter.
    /// </param>
    /// <returns>The <see cref="ILoggingBuilder"/> for chaining.</returns>
    public static ILoggingBuilder AddSingleLineThemedConsole(this ILoggingBuilder builder, Action<SingleLineThemedConsoleFormatterOptions> configure)
        => builder
            .AddThemes<SingleLineThemedConsoleFormatterOptions>()
            .AddConsole(options => options.FormatterName = SingleLineThemedConsoleFormatter.FormatterName)
            .AddConsoleFormatter<SingleLineThemedConsoleFormatter, SingleLineThemedConsoleFormatterOptions>(configure);

    /// <summary>
    /// Add a console log formatter named 'SingleLineFormatter' to the factory with default properties.
    /// </summary>
    /// <param name="builder">The <see cref="ILoggingBuilder"/> to use.</param>
    /// <returns>The <see cref="ILoggingBuilder"/> for chaining.</returns>
    public static ILoggingBuilder AddSingleLineThemedConsoleFormatter(this ILoggingBuilder builder)
        => builder
            .AddThemes<SingleLineThemedConsoleFormatterOptions>()
            .AddConsoleFormatter<SingleLineThemedConsoleFormatter, SingleLineThemedConsoleFormatterOptions>();
}
