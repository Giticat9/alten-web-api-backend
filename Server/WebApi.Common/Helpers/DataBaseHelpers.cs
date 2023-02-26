using System.Data;
using System.Reflection;

namespace WebApi.Common.Helpers;

public static class DataBaseHelpers
{
    public static DataTable ConvertModelToDataTable<T>(T model) where T : class
    {
        DataTable convertedTable = new();
        PropertyInfo[] propertiesInfo = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var property in propertiesInfo)
        {
            convertedTable.Columns.Add(property.Name);
        }

        var row = convertedTable.NewRow();

        for (int i = 0; i < propertiesInfo.Length; i++) 
        {
            row[i] = propertiesInfo[i].GetValue(model, null);
        }

        convertedTable.Rows.Add(row);

        return convertedTable;
    }

    public static DataTable ConvertArrayToDataTable<T>(IEnumerable<T> model)
    {
        DataTable convertedTable = new();
        convertedTable.Columns.Add("value");

        foreach (var item in model)
        {
            convertedTable.Rows.Add(item);
        }

        return convertedTable;
    }
}
