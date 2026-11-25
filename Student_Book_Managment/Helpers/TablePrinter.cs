using Spectre.Console;
using System.Reflection;

namespace Student_Book_Managment.Helpers
{
    static class TablePrinter
    {
        public static void PrintObj<T>(T obj, Color? rowColor = null, Color? rowBackgroundColor = null,
        Color? columnColor = null, Color? columnBackgroundColor = null, Color? borderColor = null,
        bool roundedCorners = false, string? title = null, string? caption = null) where T : class
        {
            List<PropertyInfo> properties = obj!.GetType().GetProperties().ToList();
            List<string> propertyNames = properties.Select(p => p.Name).ToList();

            PrintCustom(obj, properties, propertyNames, rowColor: rowColor, rowBackgroundColor: rowBackgroundColor, columnColor: columnColor,
            columnBackgroundColor: columnBackgroundColor, borderColor: borderColor, roundedCorners: roundedCorners, title: title, caption: caption);
        }

        public static void PrintCustom<T>(T obj, List<PropertyInfo> props, List<string> vals, bool propNameIsCol = true,
        Color? rowBackgroundColor = null, Color? columnColor = null, Color? columnBackgroundColor = null, Color? rowColor = null,
        Color? borderColor = null, bool roundedCorners = false, string? title = null, string? caption = null) where T : class
        {

            Color rowTxtClr = rowColor ?? Color.White; // defining table styling/text by checking if parameters are null, if they are, default to white/empty string.
            Color rowBgClr = rowBackgroundColor ?? Color.Default;
            Color clmnTxtClr = columnColor ?? Color.White;
            Color clmBgClr = columnBackgroundColor ?? Color.Default;
            Color brdClr = borderColor ?? Color.White;
            string ttl = title ?? string.Empty;
            string cptn = caption ?? string.Empty;

            Table table = new Table() // making table object + applying styling.
                        .BorderColor(brdClr)
                        .Title(ttl)
                        .Caption(cptn);

            if (roundedCorners)
            {
                table.RoundedBorder(); // aplying rounded borders if rounded corners param is true.
            }

            List<Text> rowCells = []; // defining list of row cells which is later going to be passed into table.AddRow() method.
                                      // if i called AddRow on each iteration it would mess up the table by adding all the property values into a single column (its meant to be 1 column per property).
            for (int i = 0; i < vals.Count; i++)
            {
                PropertyInfo prop = props[i]; // defining property variable.
                string val = vals[i]; // defining cell value variable.

                if (propNameIsCol) // if properties name is a column cell, we take the properties value as a row cells value, and properties name as a column cell value.
                {
                    string rowVal = prop.GetValue(obj)?.ToString() ?? ""; // if object's property is defined, we take that, if not, we take an empty string.
                    Text rowCell = new Text(rowVal, new Style(foreground: rowTxtClr, background: rowBgClr)); // creating text object, passing in the row cells value and styling as arguments.
                    rowCells.Add(rowCell); // added rowCell to row cells list.
                    Text columnCell = new Text(val, new Style(foreground: clmnTxtClr, background: clmBgClr)); // creating text object, passing in the column cells value and styling as arguments.
                    table.AddColumn(new TableColumn(columnCell)); // adding properties name as the column cell.
                }
                else
                {
                    string colVal = prop.GetValue(obj)?.ToString() ?? ""; // we do the opposite here (Column cell is the properties value, and Row cell is a properties name.
                    Text columnCell = new Text(colVal, new Style(foreground: clmnTxtClr, background: clmBgClr));
                    table.AddColumn(new TableColumn(columnCell));
                    rowCells.Add(new Text(val, new Style(foreground: rowTxtClr, background: rowBgClr)));
                }
            }

            table.AddRow(rowCells);
            AnsiConsole.Write(table);
        }

    }
}