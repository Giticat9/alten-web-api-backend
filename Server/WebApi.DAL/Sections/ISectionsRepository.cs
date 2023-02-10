using WebApi.BE;

namespace WebApi.DAL;

public interface ISectionsRepository
{
    Task<List<SectionModel>> GetSectionsAsync();

    Task<SectionModel> GetSectionAsync(int id);
}