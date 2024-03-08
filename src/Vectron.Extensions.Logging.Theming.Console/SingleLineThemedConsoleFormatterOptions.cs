using Microsoft.Extensions.Logging.Console;

namespace Vectron.Extensions.Logging.Theming.Console;

/// <summary>
/// Options for the single line themed console log formatter.
/// </summary>
public class SingleLineThemedConsoleFormatterOptions : ConsoleFormatterOptions, IThemedFormatterOptions
{
    /// <summary>
    /// Gets or sets a value indicating whether the whole line should be colored.
    /// </summary>
    public bool ColorWholeLine
    {
        get;
        set;
    }

    /// <summary>
    /// Gets or sets the colors theme to use.
    /// </summary>
    public string Theme { get; set; } = string.Empty;
}
