using Spectre.Console;

namespace Student_Book_Managment.Helpers.UI.Printers
{
    public static class PromptPrinter
    {
        public static T SelectionPrinter<T>(IEnumerable<T> choices, Func<T, string>? converter = null, int? pageSize = 10, string? title = null) where T : notnull
        {
            int pgSz = pageSize ?? 10;
            string ttl = title ?? string.Empty;

            SelectionPrompt<T> prompt = new SelectionPrompt<T>()
            .Title(ttl)
            .AddChoices(choices)
            .PageSize(pgSz)
            .Mode(SelectionMode.Leaf);

            if (converter != null)
            {
                prompt.UseConverter(converter);
            }

            return AnsiConsole.Prompt(prompt);
        }
    }
}
