namespace Vectron.Extensions.Logging.Theming;

/// <summary>
/// A marker interface for options used while theming.
/// </summary>
public interface IThemedFormatterOptions
{
    /// <summary>
    /// Gets a value indicating whether the whole line should be colored.
    /// </summary>
    bool ColorWholeLine
    {
        get;
    }

    /// <summary>
    /// Gets a value indicating whether includes scopes when <see langword="true" />.
    /// </summary>
    public bool IncludeScopes
    {
        get;
    }

    /// <summary>
    /// Gets the colors theme to use.
    /// </summary>
    string Theme
    {
        get;
    }

    /// <summary>
    /// Gets format string used to format timestamp in logging messages. Defaults to <see langword="null" />.
    /// </summary>
#if NET7_0_OR_GREATER
    [System.Diagnostics.CodeAnalysis.StringSyntax(System.Diagnostics.CodeAnalysis.StringSyntaxAttribute.DateTimeFormat)]
#endif

    public string? TimestampFormat
    {
        get;
    }

    /// <summary>
    /// Gets a value indicating whether or not UTC time zone should be used to format timestamps in logging messages. Defaults to <see langword="false" />.
    /// </summary>
    public bool UseUtcTimestamp
    {
        get;
    }
}
