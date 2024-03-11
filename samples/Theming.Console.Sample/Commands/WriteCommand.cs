using Microsoft.Extensions.Logging;
using SampleHelpers;
using Vectron.InteractiveConsole.Commands;

namespace Theming.Console.Sample;

/// <summary>
/// A command to generate log messages.
/// </summary>
/// <param name="logger">The <see cref="ILogger"/> to use.</param>
internal sealed class WriteCommand(ILogger<WriteCommand> logger) : IConsoleCommand
{
    private readonly ILogger logger = logger;

    /// <inheritdoc/>
    public string[]? ArgumentNames => null;

    /// <inheritdoc/>
    public string[] CommandParameters => ["write"];

    /// <inheritdoc/>
    public string HelpText => "Write text to all log levels";

    /// <inheritdoc/>
    public int MaxArguments => 0;

    /// <inheritdoc/>
    public int MinArguments => 0;

    /// <inheritdoc/>
    public void Execute(string[] arguments)
        => logger.WriteAll();
}
