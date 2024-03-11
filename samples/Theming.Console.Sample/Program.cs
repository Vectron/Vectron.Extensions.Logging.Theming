using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SampleHelpers;
using Theming.Console.Sample;
using Theming.Console.Sample.Commands;
using Vectron.Extensions.Logging.Theming.Console;
using Vectron.InteractiveConsole;
using Vectron.InteractiveConsole.AutoComplete;
using Vectron.InteractiveConsole.Commands;

var settings = new List<KeyValuePair<string, string?>>
{
    new("Logging:Console:FormatterName", "Themed"),
    new("Logging:Console:LogLevel:Default", "Trace"),
    new("Logging:Console:FormatterOptions:ColorWholeLine", "false"),
    new("Logging:Console:FormatterOptions:Theme", "MEL-Dark"),
    new("Logging:Console:FormatterOptions:IncludeScopes", "true"),
    new("Logging:Console:FormatterOptions:TimestampFormat", "HH:mm:ss"),
    new("Logging:Console:FormatterOptions:UseUtcTimestamp", "false"),
};

var builder = Host.CreateApplicationBuilder(args);

var configBuilder = builder.Configuration as IConfigurationBuilder;
configBuilder.Add(new ReloadableMemoryConfigurationSource { InitialData = settings });

builder.Logging
    .ClearProviders()
    .AddSingleLineThemedConsole();

builder.Services
    .AddInteractiveConsole()
    .AddConsoleCommand()
    .AddScoped<IConsoleCommand, ChangeThemeCommand>()
    .AddScoped<IConsoleCommand, WriteCommand>()
    .AddScoped<IConsoleCommand, ShowAllCommand>()
    .AddScoped<IAutoCompleteHandler, ThemeAutoCompleteHandler>();

using var host = builder.Build();
await host.RunAsync()
    .ConfigureAwait(false);
