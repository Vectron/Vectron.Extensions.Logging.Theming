using Microsoft.Extensions.Logging;

namespace Vectron.Extensions.Logging.Theming.Themes;

/// <summary>
/// A <see cref="ITheme"/> that uses the Microsoft.Extensions.Logging.Console colors.
/// </summary>
internal sealed class MELDarkTheme : ITheme
{
    /// <inheritdoc/>
    public string Name => "MEL-Dark";

    /// <inheritdoc/>
    public string GetCategoryColor(string category) => "\x1b[1;37m";

    /// <inheritdoc/>
    public string GetEventIdColor(EventId eventId) => "\x1b[1;37m";

    /// <inheritdoc/>
    public string GetExceptionColor(Exception exception) => "\x1b[1;37m";

    /// <inheritdoc/>
    public string GetLineColor(LogLevel logLevel) => GetLogLevelColor(logLevel);

    /// <inheritdoc/>
    public string GetLogLevelColor(LogLevel logLevel)
        => logLevel switch
        {
            LogLevel.Trace => "\x1b[37m",
            LogLevel.Debug => "\x1b[37m",
            LogLevel.Information => "\x1b[32m",
            LogLevel.Warning => "\x1b[1m\x1b[33m",
            LogLevel.Error => "\x1b[30m\x1b[41m",
            LogLevel.Critical => "\x1b[1m\x1b[37m\x1b[41m",
            LogLevel.None => string.Empty,
            _ => string.Empty,
        };

    /// <inheritdoc/>
    public string GetMessageColor(string message) => "\x1b[1;37m";

    /// <inheritdoc/>
    public string GetScopeColor(object? scope) => "\x1b[1;37m";

    /// <inheritdoc/>
    public string GetTimeColor(DateTimeOffset dateTimeOffset) => "\x1b[1;37m";
}
