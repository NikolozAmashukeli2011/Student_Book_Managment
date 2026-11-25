using Spectre.Console;
using Student_Book_Managment.Helpers.UI.Styles;

namespace Student_Book_Managment.Helpers.UI.Printers
{
    static class MarkupPrinter
    {
        private static string MarkedUpTxt(string txt, Color color, bool bold = false)
        {
            string markup = $"[{color}]{txt}[/]";

            if (bold)
            {
                markup = $"[bold {color}]{txt}[/]";
            }

            return markup;
        }

        public static void PrintSuccess(string txt)
        {
            AnsiConsole.MarkupLine(MarkedUpTxt($"{txt}\n", Color.Green, true));
        }

        public static void PrintError(string txt)
        {
            AnsiConsole.MarkupLine(MarkedUpTxt($"{txt}\n", Color.Red, true));
        }

        public static void PrintColoredHeader(string title, TablePrinterOptions opt)
        {
            var color = opt.BorderColor ?? Color.White;

            var rule = new Rule($"[bold]{Markup.Escape(title)}[/]")
            {
                Justification = Justify.Left,
                Style = new Spectre.Console.Style(color)
            };

            AnsiConsole.Write(rule);
        }
    }
}
