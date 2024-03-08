using Vectron.Extensions.Logging.Theming.Themes;

namespace Vectron.Extensions.Logging.Theming;

/// <summary>
/// The default implementation of <see cref="IThemeProvider"/> using the configured theme.
/// </summary>
/// <param name="themes">The registered themes.</param>
internal sealed class ThemeProvider(IEnumerable<ITheme> themes) : IThemeProvider
{
    // returns true on MacCatalyst
    private static bool IsAndroidOrAppleMobile => OperatingSystem.IsAndroid() || OperatingSystem.IsTvOS() || OperatingSystem.IsIOS();

    /// <inheritdoc/>
    public ITheme GetTheme(string themeName)
    {
        if (IsAndroidOrAppleMobile)
        {
            themeName = "None";
        }
        else if (string.IsNullOrEmpty(themeName))
        {
            themeName = "MEL";
        }

        var theme = themes.FirstOrDefault(x => x.Name.Equals(themeName, StringComparison.OrdinalIgnoreCase));
        return theme ?? new NoColorTheme();
    }
}
