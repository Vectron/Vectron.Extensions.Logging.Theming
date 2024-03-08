using Vectron.Extensions.Logging.TextBlock;

namespace Vectron.Extensions.Logging.Theming.TextBlock;

/// <summary>
/// Options for the single line themed text block log formatter.
/// </summary>
public class SingleLineThemedTextBlockFormatterOptions : TextBlockFormatterOptions, IThemedFormatterOptions
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
