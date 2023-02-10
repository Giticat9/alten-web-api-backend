using System.Data;
using System.Reflection;

namespace WebApi.Common.Helpers;

public class DataBaseHelpers : IDataBaseHelpers
{
    public DataTable ConvertModelToDataTable<T>(T model) where T : class
    {
        DataTable convertedTable = new DataTable();
        PropertyInfo[] propertiesInfo = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var property in propertiesInfo)
        {
            convertedTable.Columns.Add(property.Name);
        }

        var row = convertedTable.NewRow();
        var values = new object[propertiesInfo.Length];

        for (int i = 0; i < propertiesInfo.Length; i++) 
        {
            row[i] = propertiesInfo[i].GetValue(model, null);
        }

        convertedTable.Rows.Add(row);

        return convertedTable;
    }
}
