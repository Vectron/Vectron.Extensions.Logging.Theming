using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Vectron.Extensions.Logging.TextBlock.Internal;

namespace Vectron.Extensions.Logging.Theming.TextBlock;

/// <summary>
/// A themed formatter that writes colored single line messages.
/// </summary>
/// <param name="themedFormatter">The <see cref="IThemedFormatter"/> to do the formatting.</param>
public sealed class SingleLineThemedTextBlockFormatter(IThemedFormatter themedFormatter) : TextBlockFormatter(FormatterName)
{
    /// <summary>
    /// The registered name of this formatter.
    /// </summary>
    internal const string FormatterName = "SingleLineThemedFormatter";

    /// <inheritdoc/>
    public override void Write<TState>(in LogEntry<TState> logEntry, IExternalScopeProvider? scopeProvider, TextWriter textWriter)
        => themedFormatter.Write(in logEntry, scopeProvider, textWriter);
}
