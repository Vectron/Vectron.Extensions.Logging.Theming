using Vectron.Ansi;

namespace Vectron.Extensions.Logging.Theming;

/// <summary>
/// Extension methods for writing colored messages.
/// </summary>
internal static class TextWriterExtensions
{
    /// <summary>
    /// Write a colored message and reset the color at the end.
    /// </summary>
    /// <param name="textWriter">The <see cref="TextWriter"/> to use.</param>
    /// <param name="color">The ANSI color string.</param>
    /// <param name="text">The text to write.</param>
    /// <param name="colorWholeLine">Indicates if the whole line is colored.</param>
    public static void WriteColored(this TextWriter textWriter, string color, string text, bool colorWholeLine)
    {
        if (!colorWholeLine && !string.IsNullOrEmpty(color))
        {
            textWriter.Write(color);
        }

        textWriter.Write(text);

        if (!colorWholeLine && !string.IsNullOrEmpty(color))
        {
            textWriter.WriteResetColorAndStyle();
        }
    }

    /// <summary>
    /// Write a colored message and reset the color at the end.
    /// </summary>
    /// <param name="textWriter">The <see cref="TextWriter"/> to use.</param>
    /// <param name="color">The ANSI color string.</param>
    /// <param name="text">The text to write.</param>
    /// <param name="colorWholeLine">Indicates if the whole line is colored.</param>
    public static void WriteColored(this TextWriter textWriter, string color, object? text, bool colorWholeLine)
    {
        if (!colorWholeLine && !string.IsNullOrEmpty(color))
        {
            textWriter.Write(color);
        }

        textWriter.Write(text);

        if (!colorWholeLine && !string.IsNullOrEmpty(color))
        {
            textWriter.WriteResetColorAndStyle();
        }
    }

    /// <summary>
    /// Write a colored message and reset the color at the end.
    /// </summary>
    /// <param name="textWriter">The <see cref="TextWriter"/> to use.</param>
    /// <param name="color">The ANSI color string.</param>
    /// <param name="text">The text to write.</param>
    /// <param name="colorWholeLine">Indicates if the whole line is colored.</param>
    public static void WriteColored(this TextWriter textWriter, string color, Span<char> text, bool colorWholeLine)
    {
        if (!colorWholeLine && !string.IsNullOrEmpty(color))
        {
            textWriter.Write(color);
        }

        textWriter.Write(text);

        if (!colorWholeLine && !string.IsNullOrEmpty(color))
        {
            textWriter.WriteResetColorAndStyle();
        }
    }
}
