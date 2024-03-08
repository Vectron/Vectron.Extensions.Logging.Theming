using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;

namespace Vectron.Extensions.Logging.Theming;

/// <summary>
/// A base implementation of <see cref="IThemedFormatter"/>.
/// </summary>
/// <typeparam name="TOptions">The options type for this formatter.</typeparam>
public abstract class ThemedFormatter<TOptions> : IDisposable, IThemedFormatter
    where TOptions : class, IThemedFormatterOptions
{
    private readonly IDisposable? optionsReloadToken;
    private readonly IThemeProvider themeProvider;
    private bool disposed;

    /// <summary>
    /// Initializes a new instance of the <see cref="ThemedFormatter{TOptions}"/> class.
    /// </summary>
    /// <param name="options">Options for setting up this formatter.</param>
    /// <param name="themeProvider">The <see cref="IThemeProvider"/>.</param>
    protected ThemedFormatter(IOptionsMonitor<TOptions> options, IThemeProvider themeProvider)
    {
        this.themeProvider = themeProvider;
        ReloadLoggerOptions(options.CurrentValue);
        optionsReloadToken = options.OnChange(ReloadLoggerOptions);
    }

    /// <summary>
    /// Gets the current active theme.
    /// </summary>
    protected ITheme CurrentTheme
    {
        get;
        private set;
    }

    /// <summary>
    /// Gets the formatter options.
    /// </summary>
    protected TOptions FormatterOptions
    {
        get;
        private set;
    }

    /// <inheritdoc/>
    public virtual void Dispose()
    {
        if (disposed)
        {
            return;
        }

        disposed = true;
        optionsReloadToken?.Dispose();
        GC.SuppressFinalize(this);
    }

    /// <inheritdoc/>
    public abstract void Write<TState>(in LogEntry<TState> logEntry, IExternalScopeProvider? scopeProvider, TextWriter textWriter);

    [MemberNotNull(nameof(FormatterOptions))]
    [MemberNotNull(nameof(CurrentTheme))]
    private void ReloadLoggerOptions(TOptions options)
    {
        FormatterOptions = options;
        CurrentTheme = themeProvider.GetTheme(options.Theme);
    }
}
