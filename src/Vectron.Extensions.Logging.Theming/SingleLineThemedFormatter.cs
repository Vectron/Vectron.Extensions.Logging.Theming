using System.Globalization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Vectron.Ansi;

namespace Vectron.Extensions.Logging.Theming;

/// <summary>
/// A logging formatter for writing themed messages as a single line.
/// </summary>
/// <typeparam name="TOptions">The type of options class to use.</typeparam>
internal sealed class SingleLineThemedFormatter<TOptions>(
    IOptionsMonitor<TOptions> options,
    IThemeProvider themeProvider)
    : ThemedFormatter<TOptions>(options, themeProvider)
    where TOptions : class, IThemedFormatterOptions
{
    private const string InnerExceptionPrefix = " ---> ";
    private const string LogLevelPadding = ": ";
    private static readonly string MessagePadding = new(' ', GetMaximumLogLevelLength() + LogLevelPadding.Length);
    private static readonly string NewLineWithMessagePadding = Environment.NewLine + MessagePadding;

    /// <inheritdoc/>
    public override void Write<TState>(in LogEntry<TState> logEntry, IExternalScopeProvider? scopeProvider, TextWriter textWriter)
    {
        var message = logEntry.Formatter(logEntry.State, logEntry.Exception);
        if (logEntry.Exception == null && message == null)
        {
            return;
        }

        if (FormatterOptions.ColorWholeLine)
        {
            var lineColor = CurrentTheme.GetLineColor(logEntry.LogLevel);
            textWriter.Write(lineColor);
        }

        WriteTime(textWriter);
        WriteLogLevel(logEntry.LogLevel, textWriter);
        WriteCategory(logEntry.Category, textWriter);
        WriteEventId(logEntry.EventId, textWriter);
        WriteScopeInformation(textWriter, scopeProvider);
        WriteLogMessage(textWriter, message);
        WriteException(textWriter, logEntry.Exception);
        if (FormatterOptions.ColorWholeLine)
        {
            textWriter.WriteResetColorAndStyle();
        }

        textWriter.WriteLine();
    }

    private static string GetLogLevelString(LogLevel logLevel)
        => logLevel switch
        {
            LogLevel.Trace => "TRACE",
            LogLevel.Debug => "DEBUG",
            LogLevel.Information => "INFO",
            LogLevel.Warning => "WARN",
            LogLevel.Error => "FAIL",
            LogLevel.Critical => "CRIT",
            LogLevel.None => throw new ArgumentOutOfRangeException(nameof(logLevel)),
            _ => throw new ArgumentOutOfRangeException(nameof(logLevel)),
        };

    private static int GetMaximumLogLevelLength()
    {
        var length = 0;
        foreach (LogLevel level in Enum.GetValues(typeof(LogLevel)))
        {
            try
            {
                var levelString = GetLogLevelString(level);
                if (levelString.Length > length)
                {
                    length = levelString.Length;
                }
            }
            catch (ArgumentOutOfRangeException)
            {
            }
        }

        return length;
    }

    private DateTimeOffset GetCurrentDateTime()
        => FormatterOptions.UseUtcTimestamp ? DateTimeOffset.UtcNow : DateTimeOffset.Now;

    private void WriteCategory(string category, TextWriter textWriter)
    {
        if (string.IsNullOrEmpty(category))
        {
            return;
        }

        var color = CurrentTheme.GetCategoryColor(category);
        textWriter.WriteColored(color, category, FormatterOptions.ColorWholeLine);
    }

    private void WriteEventId(EventId eventId, TextWriter textWriter)
    {
        var color = CurrentTheme.GetEventIdColor(eventId);
        if (!FormatterOptions.ColorWholeLine && !string.IsNullOrEmpty(color))
        {
            textWriter.Write(color);
        }

        textWriter.Write('[');
        Span<char> span = stackalloc char[10];

        if (eventId.Id.TryFormat(span, out var charsWritten, default, CultureInfo.CurrentCulture))
        {
            textWriter.Write(span[..charsWritten]);
        }
        else
        {
            var id = eventId.Id.ToString(CultureInfo.CurrentCulture);
            textWriter.Write(id);
        }

        textWriter.Write(']');

        if (!FormatterOptions.ColorWholeLine && !string.IsNullOrEmpty(color))
        {
            textWriter.WriteResetColorAndStyle();
        }

        textWriter.Write(' ');
    }

    private void WriteException(TextWriter textWriter, Exception? exception)
    {
        if (exception == null)
        {
            return;
        }

        textWriter.Write(NewLineWithMessagePadding);
        var message = exception.ToString().Replace(Environment.NewLine, NewLineWithMessagePadding, StringComparison.Ordinal);
        var color = CurrentTheme.GetExceptionColor(exception);
        textWriter.WriteColored(color, message, FormatterOptions.ColorWholeLine);
    }

    private void WriteLogLevel(LogLevel logLevel, TextWriter textWriter)
    {
        var logLevelString = GetLogLevelString(logLevel);
        if (logLevelString == null)
        {
            return;
        }

        var color = CurrentTheme.GetLogLevelColor(logLevel);
        textWriter.WriteColored(color, logLevelString, FormatterOptions.ColorWholeLine);

        var color2 = CurrentTheme.GetMessageColor(LogLevelPadding);
        textWriter.WriteColored(color2, LogLevelPadding, FormatterOptions.ColorWholeLine);
    }

    private void WriteLogMessage(TextWriter textWriter, string message)
    {
        if (string.IsNullOrEmpty(message))
        {
            return;
        }

        var newMessage = message.Replace(Environment.NewLine, NewLineWithMessagePadding, StringComparison.Ordinal);
        var color = CurrentTheme.GetMessageColor(newMessage);
        textWriter.WriteColored(color, newMessage, FormatterOptions.ColorWholeLine);
    }

    private void WriteScopeInformation(TextWriter textWriter, IExternalScopeProvider? scopeProvider)
    {
        if (!FormatterOptions.IncludeScopes || scopeProvider == null)
        {
            return;
        }

        scopeProvider.ForEachScope(
            (scope, state) =>
            {
                var color = CurrentTheme.GetScopeColor(scope);
                state.Write("=> ");
                state.WriteColored(color, scope, FormatterOptions.ColorWholeLine);
                textWriter.Write(' ');
            },
            textWriter);
    }

    private void WriteTime(TextWriter textWriter)
    {
        var timestampFormat = FormatterOptions.TimestampFormat;
        if (string.IsNullOrEmpty(timestampFormat))
        {
            return;
        }

        var dateTimeOffset = GetCurrentDateTime();
        var timestamp = dateTimeOffset.ToString(timestampFormat, CultureInfo.CurrentCulture);

        var color = CurrentTheme.GetTimeColor(dateTimeOffset);
        textWriter.WriteColored(color, timestamp, FormatterOptions.ColorWholeLine);
        textWriter.Write(' ');
    }
}
