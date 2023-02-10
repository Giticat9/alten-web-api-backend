using System.Data;

namespace WebApi.Common.Helpers;

public interface IDataBaseHelpers
{
    DataTable ConvertModelToDataTable<T>(T model) where T : class;
}