using System.Collections.Generic;
using Avalonia.Controls;

namespace MTQtoolEditor.Controls.Helpers;

internal static class GridSizeHolder
{
    private static readonly Dictionary<ColumnDefinition, double> ColumnMinWidth = new Dictionary<ColumnDefinition, double>();
    private static readonly Dictionary<RowDefinition, double> RowMinHeight = new Dictionary<RowDefinition, double>();
    private static readonly Dictionary<ColumnDefinition, double> ColumnActualWidth = new Dictionary<ColumnDefinition, double>();
    private static readonly Dictionary<RowDefinition, double> RowActualHeight = new Dictionary<RowDefinition, double>();
    
    public static double? GetMinSize(ColumnDefinition column)
    {
        return ColumnMinWidth.ContainsKey(column) ? ColumnMinWidth[column] : null;
    }
    
    public static double? GetMinSize(RowDefinition row)
    {
        return RowMinHeight.ContainsKey(row) ? RowMinHeight[row] : null;
    }

    public static double? GetActualSize(ColumnDefinition column)
    {
        return ColumnActualWidth.ContainsKey(column) ? ColumnActualWidth[column] : null;
    }
    
    public static double? GetActualSize(RowDefinition row)
    {
        return RowActualHeight.ContainsKey(row) ? RowActualHeight[row] : null;
    }

    public static void SetSize(ColumnDefinition column)
    {
        ColumnMinWidth[column] = column.MinWidth;
        ColumnActualWidth[column] = column.ActualWidth;
    }
    
    public static void SetSize(RowDefinition row)
    {
        RowMinHeight[row] = row.MinHeight;
        RowActualHeight[row] = row.ActualHeight;
    }
}