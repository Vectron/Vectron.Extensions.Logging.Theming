using Microsoft.Extensions.Logging;
using Vectron.Extensions.Logging.TextBlock;
using Vectron.Extensions.Logging.TextBlock.Internal;

namespace Vectron.Extensions.Logging.Theming.TextBlock;

/// <summary>
/// Extensions for setting up the logging builder.
/// </summary>
public static class TextBlockLoggerExtensions
{
    /// <summary>
    /// Add the console log formatter named 'SingleLineFormatter' to the factory with default properties.
    /// </summary>
    /// <param name="builder">The <see cref="ILoggingBuilder"/> to use.</param>
    /// <returns>The <see cref="ILoggingBuilder"/> for chaining.</returns>
    public static ILoggingBuilder AddSingleLineThemedTextBlock(this ILoggingBuilder builder)
        => builder
            .AddTextBlock(options => options.FormatterName = SingleLineThemedTextBlockFormatter.FormatterName)
            .AddSingleLineThemedTextBlockFormatter();

    /// <summary>
    /// Add and configure a console log formatter named 'SingleLineFormatter' to the factory.
    /// </summary>
    /// <param name="builder">The <see cref="ILoggingBuilder"/> to use.</param>
    /// <param name="configure">
    /// A delegate to configure the <see cref="TextBlockLogger"/> options for the built-in default log formatter.
    /// </param>
    /// <returns>The <see cref="ILoggingBuilder"/> for chaining.</returns>
    public static ILoggingBuilder AddSingleLineThemedTextBlock(this ILoggingBuilder builder, Action<SingleLineThemedTextBlockFormatterOptions> configure)
        => builder
            .AddThemes<SingleLineThemedTextBlockFormatterOptions>()
            .AddTextBlock(options => options.FormatterName = SingleLineThemedTextBlockFormatter.FormatterName)
            .AddTextBlockFormatter<SingleLineThemedTextBlockFormatter, SingleLineThemedTextBlockFormatterOptions>(configure);

    /// <summary>
    /// Add a console log formatter named 'SingleLineFormatter' to the factory with default properties.
    /// </summary>
    /// <param name="builder">The <see cref="ILoggingBuilder"/> to use.</param>
    /// <returns>The <see cref="ILoggingBuilder"/> for chaining.</returns>
    public static ILoggingBuilder AddSingleLineThemedTextBlockFormatter(this ILoggingBuilder builder)
        => builder
            .AddThemes<SingleLineThemedTextBlockFormatterOptions>()
            .AddTextBlockFormatter<SingleLineThemedTextBlockFormatter, SingleLineThemedTextBlockFormatterOptions>();
}
