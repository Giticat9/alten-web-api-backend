using WebApi.BE.Account;

namespace WebApi.DAL
{
    public interface IAccountsRepository
    {
        Task<List<AccountInfoModel>> GetAll(string? term);

        Task<AccountInfoModel> GetByGuid(Guid guid);

        Task<bool> AddOrUpdate(AccountInfoRequestModel model);

        Task<Guid> DeleteByGuid (Guid guid);

        Task<int> GetAccountsCountAll();

        Task<bool> CheckAccountExists(string? login, string? email);
    }
}
