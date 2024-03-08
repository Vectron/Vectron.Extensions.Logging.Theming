using Vectron.Extensions.Logging.Theming.Themes;

namespace Vectron.Extensions.Logging.Theming;

/// <summary>
/// A <see cref="ITheme"/> provider.
/// </summary>
public interface IThemeProvider
{
    /// <summary>
    /// Get the theme for the given name.
    /// </summary>
    /// <param name="themeName">The name of the theme.</param>
    /// <returns>The requested theme, or <see cref="NoColorTheme"/> if not found.</returns>
    ITheme GetTheme(string themeName);
}
