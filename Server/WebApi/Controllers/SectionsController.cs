using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.BE;
using WebApi.BL;

namespace WebApi.Controllers;

[ApiController]
[Route("api/sections")]
public class SectionsController : ControllerBase
{
    private readonly ISectionBL _sectionBL;
    
    public SectionsController(ISectionBL sectionBL)
    {
        _sectionBL = sectionBL;
    }

    [Authorize]
    [HttpGet]
    public async Task<List<SectionModel>> GetAll()
    {
        var sections = await _sectionBL.GetSectionsAsync();

        return sections;
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<SectionModel> GetById(int id)
    {
        var section = await _sectionBL.GetSectionAsync(id);

        return section;
    }
}