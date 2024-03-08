using Microsoft.Extensions.Logging;

namespace Vectron.Extensions.Logging.Theming.Themes;

/// <summary>
/// A <see cref="ITheme"/> that uses the Microsoft.Extensions.Logging.Console colors.
/// </summary>
internal sealed class NLogTheme : ITheme
{
    /// <inheritdoc/>
    public string Name => "NLog";

    /// <inheritdoc/>
    public string GetCategoryColor(string category) => string.Empty;

    /// <inheritdoc/>
    public string GetEventIdColor(EventId eventId) => string.Empty;

    /// <inheritdoc/>
    public string GetExceptionColor(Exception exception) => string.Empty;

    /// <inheritdoc/>
    public string GetLineColor(LogLevel logLevel) => GetLogLevelColor(logLevel);

    /// <inheritdoc/>
    public string GetLogLevelColor(LogLevel logLevel)
        => logLevel switch
        {
            LogLevel.Trace => "\x1b[90m",
            LogLevel.Debug => "\x1b[37m",
            LogLevel.Information => "\x1b[97m",
            LogLevel.Warning => "\x1b[95m",
            LogLevel.Error => "\x1b[93m",
            LogLevel.Critical => "\x1b[91m",
            LogLevel.None => string.Empty,
            _ => string.Empty,
        };

    /// <inheritdoc/>
    public string GetMessageColor(string message) => string.Empty;

    /// <inheritdoc/>
    public string GetScopeColor(object? scope) => string.Empty;

    /// <inheritdoc/>
    public string GetTimeColor(DateTimeOffset dateTimeOffset) => string.Empty;
}
