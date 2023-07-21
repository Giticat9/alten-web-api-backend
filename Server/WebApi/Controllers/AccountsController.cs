using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebApi.BE.Account;
using WebApi.BL;
using WebApi.Common;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/accounts")]
    [SwaggerTag("Работа с учетными записями")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountsBL _accountsBL;

        public AccountsController(IAccountsBL accountsBL)
        {
            _accountsBL = accountsBL;
        }

        [Authorize]
        [HttpGet("")]
        [Produces(MediaTypes.ApplicationJson)]
        [SwaggerOperation(Summary = "Получения списока учетных записей")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Успешное получение учетных записей")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Необходима авторизация")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Внутренняя ошибка сервера", Type = typeof(HttpErrorDescription))]
        public async Task<ActionResult<List<AccountInfoModel>>> GetAll([FromQuery] string term = "")
        {
            var accounts = await _accountsBL.GetAll(term);

            return accounts;
        }

        [Authorize]
        [HttpGet("{guid}")]
        [Produces(MediaTypes.ApplicationJson)]
        [SwaggerOperation(Summary = "Получения учетной записи по идентификатору")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Успешное получение учетной записи")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Необходима авторизация")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Внутренняя ошибка сервера", Type = typeof(HttpErrorDescription))]
        public async Task<ActionResult<AccountInfoModel>> GetByGuid(Guid guid)
        {
            var account = await _accountsBL.GetByGuid(guid);

            return account;
        }

        [Authorize]
        [HttpPost("AddOrUpdate")]
        [Produces(MediaTypes.ApplicationJson)]
        [SwaggerOperation(Summary = "Добавление или изменение учетной записи")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Успешное добавление или изменение учетной записи")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Необходима авторизация")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Внутренняя ошибка сервера", Type = typeof(HttpErrorDescription))]
        public async Task<ActionResult<bool>> AddOrUpdate([FromBody] AccountInfoRequestModel model)
        {
            var isUserExists = await _accountsBL.CheckAccountExists(model.Login, model.Email);

            if (isUserExists)
            {
                throw new HttpException(StatusCodes.Status409Conflict, 
                    "Пользователь с таким логином или эл-почтой уже существует");
            }

            var isUserCreated = await _accountsBL.AddOrUpdate(model);

            return isUserCreated;
        }

        [Authorize]
        [HttpDelete("{guid}")]
        [Produces(MediaTypes.ApplicationJson)]
        [SwaggerOperation(Summary = "Удаление учетной записи")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Успешное удаление учетной записи")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Необходима авторизация")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Недостаточно прав для удаления учетной записи")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Внутренняя ошибка сервера", Type = typeof(HttpErrorDescription))]
        public async Task<ActionResult<Guid>> DeleteByGuid(Guid guid)
        {
            var accountsCount = await _accountsBL.GetAccountsCountAll();

            if (accountsCount == 1)
            {
                throw new HttpException(StatusCodes.Status403Forbidden,
                    "Невозможно удалить учетную запись, т.к. для удаления должно быть более одной записи");
            }

            var accounts = await _accountsBL.GetAll();
            var filteredAccounts = accounts
                .Select(account => account)
                .Where(account => account.Guid != guid)
                .ToList();

            var isAccountsAccessedExists = filteredAccounts
                .Where(account =>
                    account.Permissions.IsAdministrator ||
                    account.Permissions.IsAccountFullAccess ||
                    account.Permissions.IsAccountCreate ||
                    account.Permissions.IsAccountEdit
                )
                .ToList()
                .Count > 1;

            if(!isAccountsAccessedExists)
            {
                throw new HttpException(StatusCodes.Status403Forbidden,
                    "Невозможно удалить учетную запись, т.к. для удаления необходимо чтобы " +
                    "одна или более записей имена права на создание или изменения учетной записи");
            }

            var deletedGuid = await _accountsBL.DeleteByGuid(guid);

            return deletedGuid;
        }
    }
}
