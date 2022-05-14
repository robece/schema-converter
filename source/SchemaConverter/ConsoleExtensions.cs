using McMaster.Extensions.CommandLineUtils;

namespace SchemaConverter
{
    public static class ConsoleExtensions
    {
        public static void WriteLine(this IConsole console, string text, ConsoleColor foregroundColor)
        {
            ConsoleColor original = console.ForegroundColor;
            console.ForegroundColor = foregroundColor;
            console.WriteLine(text);
            console.ForegroundColor = original;
        }
    }
}
