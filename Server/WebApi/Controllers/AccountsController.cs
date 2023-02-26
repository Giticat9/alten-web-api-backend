using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Base;
using WebApi.BE.Account;
using WebApi.BL;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountsBL _accountsBL;

        public AccountsController(IAccountsBL accountsBL)
        {
            _accountsBL = accountsBL;
        }

        [Authorize]
        [HttpGet("")]
        public async Task<BaseApiModel> GetAll([FromQuery] string term = "")
        {
            var accounts = await _accountsBL.GetAll(term);

            return DataApiModel.Ok(accounts);
        }

        [Authorize]
        [HttpGet("{guid}")]
        public async Task<BaseApiModel> GetByGuid(Guid guid)
        {
            var account = await _accountsBL.GetByGuid(guid);

            return DataApiModel.Ok(account);
        }

        [HttpPost("AddOrUpdate")]
        public async Task<BaseApiModel> AddOrUpdate([FromForm] AccountInfoRequestModel model)
        {
            var isUserExists = await _accountsBL.CheckAccountExists(model.Login, model.Email);

            if (isUserExists)
            {
                return DataApiModel.Error(
                    "Пользователь с таким логином или эл-почтой уже существует",
                    ErrorCode.AcccountIsExists
                );
            }

            var isUserCreated = await _accountsBL.AddOrUpdate(model);

            return DataApiModel.Ok(isUserCreated);
        }

        [HttpDelete("{guid}")]
        public async Task<BaseApiModel> DeleteByGuid(Guid guid)
        {
            var accountsCount = await _accountsBL.GetAccountsCountAll();

            if (accountsCount == 1)
            {
                return DataApiModel.Error(
                    "Невозможно удалить учетную запись, т.к. для удаления должно быть более одной записи", 
                    ErrorCode.FewAccountsToDelete
                );
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
                return DataApiModel.Error(
                    "Невозможно удалить учетную запись, т.к. для удаления необходимо чтобы " +
                    "одна или более записей имена права на создание или изменения учетной записи",
                    ErrorCode.NoAccountsWithRights
                );
            }

            var deletedGuid = await _accountsBL.DeleteByGuid(guid);

            return DataApiModel.Ok(deletedGuid);
        }
    }
}
