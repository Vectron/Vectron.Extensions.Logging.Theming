using Microsoft.Extensions.Logging;

namespace SampleHelpers;

/// <summary>
/// All available log messages.
/// </summary>
public static partial class LogMessages
{
    /// <summary>
    /// Write a message to all log levels.
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/> to use.</param>
    public static void WriteAll(this ILogger logger)
    {
        logger.WriteTraceMessage();
        logger.WriteDebugMessage();
        logger.WriteInformationMessage();
        logger.WriteWarningMessage();
        logger.WriteErrorMessage();
        logger.WriteCriticalMessage();
        var testException = new NotSupportedException();
        logger.WriteExceptionMessage(testException);
    }

    /// <summary>
    /// Write a critical message to the logger.
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
    [LoggerMessage(5, LogLevel.Critical, "This is a critical message")]
    public static partial void WriteCriticalMessage(this ILogger logger);

    /// <summary>
    /// Write a debug message to the logger.
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
    [LoggerMessage(1, LogLevel.Debug, "This is a debug message")]
    public static partial void WriteDebugMessage(this ILogger logger);

    /// <summary>
    /// Write a error message to the logger.
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
    [LoggerMessage(4, LogLevel.Error, "This is a error message")]
    public static partial void WriteErrorMessage(this ILogger logger);

    /// <summary>
    /// Write a critical message with an exception to the logger.
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
    /// <param name="exception">The exception to log.</param>
    [LoggerMessage(6, LogLevel.Critical, "Message with exception")]
    public static partial void WriteExceptionMessage(this ILogger logger, Exception exception);

    /// <summary>
    /// Write a information message to the logger.
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
    [LoggerMessage(2, LogLevel.Information, "This is a information message")]
    public static partial void WriteInformationMessage(this ILogger logger);

    /// <summary>
    /// Write a trace message to the logger.
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
    [LoggerMessage(0, LogLevel.Trace, "This is a trace message")]
    public static partial void WriteTraceMessage(this ILogger logger);

    /// <summary>
    /// Write a warning message to the logger.
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
    [LoggerMessage(3, LogLevel.Warning, "This is a warning message")]
    public static partial void WriteWarningMessage(this ILogger logger);
}
