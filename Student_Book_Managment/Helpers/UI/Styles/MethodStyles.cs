using Spectre.Console;
using Student_Book_Managment.Helpers.UI.Styles;

namespace Student_Book_Managment.Helpers.Printers.TableStyles
{
    static class MethodStyles
    {
        public static readonly TablePrinterOptions AddStyles = new()
        {
            ColumnColor = Color.LightGreen,
            RoundedCorners = true,
            BorderColor = Color.Green
        };

        public static readonly TablePrinterOptions UpdateStyles = new()
        {
            ColumnColor = Color.LightYellow3,
            RoundedCorners = true,
            BorderColor = Color.DarkGoldenrod
        };


        public static readonly TablePrinterOptions DisplayStyles = new()
        {
            ColumnColor = Color.Aqua,
            RoundedCorners = true,
            BorderColor = Color.Blue
        };

        public static readonly TablePrinterOptions FindStyles = new()
        {
            ColumnColor = Color.Orchid1,
            RoundedCorners = true,
            BorderColor = Color.DarkMagenta
        };

        public static readonly TablePrinterOptions RemoveStyles = new()
        {
            ColumnColor = Color.LightCoral,
            RoundedCorners = true,
            BorderColor = Color.DarkRed
        };
    }
}