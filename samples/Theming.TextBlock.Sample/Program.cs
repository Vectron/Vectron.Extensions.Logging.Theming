using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SampleHelpers;
using Vectron.Extensions.Hosting.Wpf;
using Vectron.Extensions.Logging.Theming.TextBlock;
using Vectron.Extensions.Logging.Theming.TextBlock.Sample;

var builder = Host.CreateApplicationBuilder(args);

var settings = new List<KeyValuePair<string, string?>>
{
    new("Logging:TextBlock:FormatterName", "Themed"),
    new("Logging:TextBlock:LogLevel:Default", "Trace"),
    new("Logging:TextBlock:MaxMessages", "50"),
    new("Logging:TextBlock:FormatterOptions:ColorWholeLine", "false"),
    new("Logging:TextBlock:FormatterOptions:Theme", "MEL"),
    new("Logging:TextBlock:FormatterOptions:IncludeScopes", "true"),
    new("Logging:TextBlock:FormatterOptions:TimestampFormat", "HH:mm:ss"),
    new("Logging:TextBlock:FormatterOptions:UseUtcTimestamp", "false"),
};

var configBuilder = builder.Configuration as IConfigurationBuilder;
configBuilder.Add(new ReloadableMemoryConfigurationSource { InitialData = settings });

builder.Logging
    .ClearProviders()
    .AddSingleLineThemedTextBlock();

builder.Services
    .AddWpf<App, MainWindow, MainWindowViewModel>()
    .AddResourceDictionary("pack://application:,,,/MahApps.Metro;component/Styles/Controls.xaml")
    .AddResourceDictionary("pack://application:,,,/MahApps.Metro;component/Styles/Fonts.xaml")
    .AddResourceDictionary("pack://application:,,,/MahApps.Metro;component/Styles/Themes/Light.Steel.xaml");

_ = builder.Services;

using var host = builder.Build();
await host.RunAsync().ConfigureAwait(false);
