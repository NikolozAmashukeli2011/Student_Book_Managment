using Spectre.Console;

namespace Student_Book_Managment.Helpers.UI.Styles
{
    public record TablePrinterOptions
    {
        public Color? RowColor {  get; init; }
        public Color? RowBackgroundColor { get; init; }
        public Color? ColumnColor { get; init; }
        public Color? ColumnBackgroundColor { get; init; }
        public Color? BorderColor { get; init; }
        public bool RoundedCorners { get; init; }
        public string? Title { get; init; }
        public string? Caption { get; init; }
    }
}
