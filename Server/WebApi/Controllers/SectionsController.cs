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
    [SwaggerTag("Работа с разделами")]
    public class SectionsController : ControllerBase
    {
        private readonly ISectionBL _sectionBL;
        
        public SectionsController(ISectionBL sectionBL)
        {
            _sectionBL = sectionBL;
        }

        [Authorize]
        [HttpGet]
        [SwaggerOperation(Summary = "Получение всех разделов")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Успешное получение разделов")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Необходима авторизация")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Внутренняя ошибка сервера", Type = typeof(HttpErrorDescription))]
        public async Task<ActionResult<List<SectionModel>>> GetAll()
        {
            var sections = await _sectionBL.GetSectionsAsync();

            return sections;
        }

        [Authorize]
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Получение раздела по идентификатору")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Успешное получения раздела")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Необходима авторизация")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Внутренняя ошибка сервера", Type = typeof(HttpErrorDescription))]
        public async Task<ActionResult<SectionModel>> GetById(int id)
        {
            var section = await _sectionBL.GetSectionAsync(id);

            return section;
        }
    }
}