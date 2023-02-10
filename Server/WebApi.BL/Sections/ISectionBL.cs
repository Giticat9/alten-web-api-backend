using WebApi.BE;

namespace WebApi.BL;

public interface ISectionBL
{
    Task<List<SectionModel>> GetSectionsAsync();
    Task<SectionModel> GetSectionAsync(int id);
}