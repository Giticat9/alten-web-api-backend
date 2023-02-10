using System.Data;
using WebApi.BE;
using WebApi.BE.Mappers;
using WebApi.Common;

namespace WebApi.DAL;

public class SectionRepository : ISectionsRepository
{
    private readonly IWebApiDatabase _webApiDatabase;
    private readonly ISectionMapper _sectionMapper;
    
    public SectionRepository(
        IWebApiDatabase webApiDatabase,
        ISectionMapper sectionMapper
    )
    {
        _webApiDatabase = webApiDatabase;
        _sectionMapper = sectionMapper;
    }

    public async Task<List<SectionModel>> GetSectionsAsync()
    {
        try
        {
            using (var connection =  await _webApiDatabase.UseWebApiDatabaseAsync())
            using (var command = connection.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "[dbo].[spSectionsGet]";

                using (var reader = await command.ExecuteReaderAsync())
                {
                    var result = new List<SectionModel>();
                    
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            var section = _sectionMapper.MapReaderToSectionModel(reader);
                            result.Add(section);
                        }

                        return result;
                    }

                    return result;
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Ошибка получения списка разделов", ex);
        }
    }
    
    public async Task<SectionModel> GetSectionAsync(int id)
    {
        try
        {
            using (var connection = await _webApiDatabase.UseWebApiDatabaseAsync())
            using (var command = connection.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "[dbo].[spSectionsGet]";
                command.Parameters.AddWithValue("@id", id);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (reader.HasRows && await reader.ReadAsync())
                    {
                        return _sectionMapper.MapReaderToSectionModel(reader);
                    } 

                    return new SectionModel();
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Ошибка получения раздела", ex);
        }
    }
}