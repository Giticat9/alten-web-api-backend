using WebApi.BE.Account;
using WebApi.DAL;

namespace WebApi.BL
{
    public class AccountsBL : IAccountsBL
    {
        private readonly IAccountsRepository _accountsRepository;

        public AccountsBL(IAccountsRepository accountsRepository)
        {
            _accountsRepository = accountsRepository;
        }

        public async Task<List<AccountInfoModel>> GetAll(string? term = null)
        {
            try
            {
                return await _accountsRepository.GetAll(term);
            }
            catch
            {
                throw;
            }
        }

        public async Task<AccountInfoModel> GetByGuid(Guid guid)
        {
            try
            {
                return await _accountsRepository.GetByGuid(guid);
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> AddOrUpdate(AccountInfoRequestModel model)
        {
            try
            {
                return await _accountsRepository.AddOrUpdate(model);
            }
            catch
            {
                throw;
            }
        }

        public async Task<Guid> DeleteByGuid(Guid guid)
        {
            try
            {
                return await _accountsRepository.DeleteByGuid(guid);
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> GetAccountsCountAll()
        {
            try
            {
                return await _accountsRepository.GetAccountsCountAll();
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> CheckAccountExists(string? login, string? email)
        {
            try
            {
                return await _accountsRepository.CheckAccountExists(login, email);
            } 
            catch
            {
                throw;
            }
        }
    }
}
