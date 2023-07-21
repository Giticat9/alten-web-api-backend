using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebApi.BE;
using WebApi.BL;
using WebApi.Common;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/sections")]
    [SwaggerTag("������ � ���������")]
    public class SectionsController : ControllerBase
    {
        private readonly ISectionBL _sectionBL;
        
        public SectionsController(ISectionBL sectionBL)
        {
            _sectionBL = sectionBL;
        }

        [Authorize]
        [HttpGet]
        [SwaggerOperation(Summary = "��������� ���� ��������")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "�������� ��������� ��������")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "���������� �����������")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "���������� ������ �������", Type = typeof(HttpErrorDescription))]
        public async Task<ActionResult<List<SectionModel>>> GetAll()
        {
            var sections = await _sectionBL.GetSectionsAsync();

            return sections;
        }

        [Authorize]
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "��������� ������� �� ��������������")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "�������� ��������� �������")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "���������� �����������")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "���������� ������ �������", Type = typeof(HttpErrorDescription))]
        public async Task<ActionResult<SectionModel>> GetById(int id)
        {
            var section = await _sectionBL.GetSectionAsync(id);

            return section;
        }
    }
}