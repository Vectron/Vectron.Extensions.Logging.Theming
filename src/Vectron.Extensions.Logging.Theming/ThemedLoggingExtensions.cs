using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Vectron.Extensions.Logging.Theming.Themes;

namespace Vectron.Extensions.Logging.Theming;

/// <summary>
/// Extensions for setting up the logging builder.
/// </summary>
public static class ThemedLoggingExtensions
{
    /// <summary>
    /// Add theming support.
    /// </summary>
    /// <typeparam name="TOptions">The options type to use.</typeparam>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
    /// <returns>The <see cref="IServiceCollection"/> for chaining.</returns>
    public static IServiceCollection AddThemes<TOptions>(this IServiceCollection services)
        where TOptions : class, IThemedFormatterOptions
    {
        services.TryAddEnumerable(ServiceDescriptor.Singleton<ITheme, NoColorTheme>());
        services.TryAddEnumerable(ServiceDescriptor.Singleton<ITheme, MELTheme>());
        services.TryAddEnumerable(ServiceDescriptor.Singleton<ITheme, MELDarkTheme>());
        services.TryAddEnumerable(ServiceDescriptor.Singleton<ITheme, NLogTheme>());
        services.TryAddEnumerable(ServiceDescriptor.Singleton<ITheme, NLogDarkTheme>());
        services.TryAddEnumerable(ServiceDescriptor.Singleton<ITheme, SerilogTheme>());
        services.TryAddEnumerable(ServiceDescriptor.Singleton<ITheme, SerilogDarkTheme>());

        services.TryAddSingleton<IThemeProvider, ThemeProvider>();

        services.TryAddSingleton<IThemedFormatter, SingleLineThemedFormatter<TOptions>>();

        return services;
    }

    /// <summary>
    /// Add theming support.
    /// </summary>
    /// <typeparam name="TOptions">The options type to use.</typeparam>
    /// <param name="builder">The <see cref="ILoggingBuilder"/> to configure.</param>
    /// <returns>The <see cref="ILoggingBuilder"/> for chaining.</returns>
    public static ILoggingBuilder AddThemes<TOptions>(this ILoggingBuilder builder)
        where TOptions : class, IThemedFormatterOptions
    {
        _ = builder.Services.AddThemes<TOptions>();
        return builder;
    }
}
