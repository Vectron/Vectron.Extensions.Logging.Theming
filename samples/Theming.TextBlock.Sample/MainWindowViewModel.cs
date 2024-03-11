using System.Collections.ObjectModel;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ControlzEx.Theming;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SampleHelpers;

namespace Vectron.Extensions.Logging.Theming.TextBlock.Sample;

/// <summary>
/// The main windows view model.
/// </summary>
internal sealed partial class MainWindowViewModel : ObservableObject
{
    private readonly IConfiguration configuration;
    private readonly ILogger<MainWindowViewModel> logger;
    private readonly ThemeManager themeManager;

    [ObservableProperty]
    private IEnumerable<Theme> applicationThemes;

    [ObservableProperty]
    private IEnumerable<string> loggingThemes;

    [ObservableProperty]
    private Theme? selectedApplicationTheme;

    [ObservableProperty]
    private string? selectedLoggingTheme;

    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
    /// </summary>
    /// <param name="logger">A <see cref="ILogger"/>.</param>
    /// <param name="configuration">The <see cref="IConfiguration"/>.</param>
    /// <param name="loggingThemes">The registered <see cref="ITheme"/>.</param>
    public MainWindowViewModel(ILogger<MainWindowViewModel> logger, IConfiguration configuration, IEnumerable<ITheme> loggingThemes)
    {
        this.logger = logger;
        this.configuration = configuration;

        themeManager = ThemeManager.Current;
        var sortedThemes = themeManager.Themes.OrderBy(x => x.ColorScheme, StringComparer.OrdinalIgnoreCase).ThenBy(x => x.BaseColorScheme, StringComparer.OrdinalIgnoreCase);
        applicationThemes = new ReadOnlyObservableCollection<Theme>(new ObservableCollection<Theme>(sortedThemes));
        selectedApplicationTheme = themeManager.DetectTheme() ?? applicationThemes.First();

        var themesCollection = new ObservableCollection<string>(loggingThemes.Select(x => x.Name));
        this.loggingThemes = new ReadOnlyObservableCollection<string>(themesCollection);
        selectedLoggingTheme = configuration["Logging:TextBlock:FormatterOptions:Theme"] ?? "None";
    }

    [RelayCommand]
    private void ChangeApplicationTheme(Theme theme) => _ = themeManager.ChangeTheme(Application.Current, theme);

    [RelayCommand]
    private void ChangeLoggingTheme(string theme) => configuration["Logging:TextBlock:FormatterOptions:Theme"] = theme;

    [RelayCommand]
    private async Task DemoAll()
    {
        foreach (var theme in LoggingThemes)
        {
            configuration["Logging:Console:FormatterOptions:Theme"] = theme;
            configuration["Logging:Console:FormatterOptions:IncludeScopes"] = "false";
            configuration["Logging:Console:FormatterOptions:ColorWholeLine"] = "false";
            await Task.Delay(1000).ConfigureAwait(false);
            LogOptionsChanged(theme, includeScopes: false, colorWholeLine: false);
            logger.WriteAll();

            configuration["Logging:Console:FormatterOptions:IncludeScopes"] = "true";
            configuration["Logging:Console:FormatterOptions:ColorWholeLine"] = "false";
            LogOptionsChanged(theme, includeScopes: true, colorWholeLine: false);
            logger.WriteAll();

            configuration["Logging:Console:FormatterOptions:IncludeScopes"] = "false";
            configuration["Logging:Console:FormatterOptions:ColorWholeLine"] = "true";
            LogOptionsChanged(theme, includeScopes: false, colorWholeLine: true);
            logger.WriteAll();

            configuration["Logging:Console:FormatterOptions:IncludeScopes"] = "false";
            configuration["Logging:Console:FormatterOptions:ColorWholeLine"] = "true";
            LogOptionsChanged(theme, includeScopes: true, colorWholeLine: true);
            logger.WriteAll();
        }
    }

    [RelayCommand]
    private void GenerateLogMessages()
        => logger.WriteAll();

    [LoggerMessage(10, LogLevel.Information, "Theme: {Theme}, IncludeScopes: {IncludeScopes}, ColorWholeLine: {ColorWholeLine}")]
    private partial void LogOptionsChanged(string theme, bool includeScopes, bool colorWholeLine);
}
