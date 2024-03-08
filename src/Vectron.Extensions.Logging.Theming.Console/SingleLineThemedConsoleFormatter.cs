using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;

namespace Vectron.Extensions.Logging.Theming.Console;

/// <summary>
/// A themed formatter that writes colored single line messages.
/// </summary>
public sealed class SingleLineThemedConsoleFormatter : ConsoleFormatter, IDisposable
{
    /// <summary>
    /// The registered name of this formatter.
    /// </summary>
    internal const string FormatterName = "SingleLineThemedFormatter";

    private readonly IDisposable? optionsReloadToken;
    private readonly IThemedFormatter themedFormatter;

    /// <summary>
    /// Initializes a new instance of the <see cref="SingleLineThemedConsoleFormatter"/> class.
    /// </summary>
    /// <param name="options">Options for setting up this formatter.</param>
    /// <param name="themedFormatter">The <see cref="IThemedFormatter"/> to do the formatting.</param>
    public SingleLineThemedConsoleFormatter(IOptionsMonitor<SingleLineThemedConsoleFormatterOptions> options, IThemedFormatter themedFormatter)
        : base(FormatterName)
    {
        ReloadLoggerOptions(options.CurrentValue);
        optionsReloadToken = options.OnChange(ReloadLoggerOptions);
        this.themedFormatter = themedFormatter;
    }

    /// <summary>
    /// Gets or sets the formatter options.
    /// </summary>
    internal SingleLineThemedConsoleFormatterOptions FormatterOptions
    {
        get;
        set;
    }

    /// <inheritdoc/>
    public void Dispose()
        => optionsReloadToken?.Dispose();

    /// <inheritdoc/>
    public override void Write<TState>(in LogEntry<TState> logEntry, IExternalScopeProvider? scopeProvider, TextWriter textWriter)
        => themedFormatter.Write(in logEntry, scopeProvider, textWriter);

    [MemberNotNull(nameof(FormatterOptions))]
    private void ReloadLoggerOptions(SingleLineThemedConsoleFormatterOptions options) => FormatterOptions = options;
}
