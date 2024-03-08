using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Vectron.Extensions.Logging.Theming;

/// <summary>
/// A formatter to write messages with a colored theme.
/// </summary>
public interface IThemedFormatter
{
    /// <summary>
    /// Writes the log message to the specified TextWriter.
    /// </summary>
    /// <remarks>
    /// if the formatter wants to write colors to the console, it can do so by embedding ANSI color codes into the string.
    /// </remarks>
    /// <param name="logEntry">The log entry.</param>
    /// <param name="scopeProvider">The provider of scope data.</param>
    /// <param name="textWriter">The string writer embedding ANSI code for colors.</param>
    /// <typeparam name="TState">The type of the object to be written.</typeparam>
    void Write<TState>(in LogEntry<TState> logEntry, IExternalScopeProvider? scopeProvider, TextWriter textWriter);
}
