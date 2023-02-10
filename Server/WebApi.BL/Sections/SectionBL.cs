using WebApi.BE;
using WebApi.DAL;

namespace WebApi.BL;

public class SectionBL : ISectionBL
{
    private readonly ISectionsRepository _sectionsRepository;

    public SectionBL(ISectionsRepository sectionsRepository)
    {
        _sectionsRepository = sectionsRepository;
    }
    
    public async Task<List<SectionModel>> GetSectionsAsync()
    {
        try
        {
            return await _sectionsRepository.GetSectionsAsync();
        }
        catch
        {
            throw;
        }
    }
    
    public async Task<SectionModel> GetSectionAsync(int id)
    {
        try
        {
            return await _sectionsRepository.GetSectionAsync(id);
        }
        catch
        {
            throw;
        }
    }
}