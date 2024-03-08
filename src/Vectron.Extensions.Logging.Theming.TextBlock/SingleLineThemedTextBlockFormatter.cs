using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Vectron.Extensions.Logging.TextBlock.Internal;

namespace Vectron.Extensions.Logging.Theming.TextBlock;

/// <summary>
/// A themed formatter that writes colored single line messages.
/// </summary>
public sealed class SingleLineThemedTextBlockFormatter : TextBlockFormatter, IDisposable
{
    /// <summary>
    /// The registered name of this formatter.
    /// </summary>
    internal const string FormatterName = "SingleLineThemedFormatter";

    private readonly IDisposable? optionsReloadToken;
    private readonly IThemedFormatter themedFormatter;

    /// <summary>
    /// Initializes a new instance of the <see cref="SingleLineThemedTextBlockFormatter"/> class.
    /// </summary>
    /// <param name="options">Options for setting up this formatter.</param>
    /// <param name="themedFormatter">The <see cref="IThemedFormatter"/> to do the formatting.</param>
    public SingleLineThemedTextBlockFormatter(IOptionsMonitor<SingleLineThemedTextBlockFormatterOptions> options, IThemedFormatter themedFormatter)
        : base(FormatterName)
    {
        ReloadLoggerOptions(options.CurrentValue);
        optionsReloadToken = options.OnChange(ReloadLoggerOptions);
        this.themedFormatter = themedFormatter;
    }

    /// <summary>
    /// Gets or sets the formatter options.
    /// </summary>
    internal SingleLineThemedTextBlockFormatterOptions FormatterOptions
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
    private void ReloadLoggerOptions(SingleLineThemedTextBlockFormatterOptions options) => FormatterOptions = options;
}
