using WebApi.BE.Account;

namespace WebApi.BL
{
    public interface IAccountsBL
    {
        Task<List<AccountInfoModel>> GetAll(string? term = null);

        Task<AccountInfoModel> GetByGuid(Guid guid);

        Task<bool> AddOrUpdate(AccountInfoRequestModel model);

        Task<Guid> DeleteByGuid (Guid guid);

        Task<int> GetAccountsCountAll();

        Task<bool> CheckAccountExists(string? login, string? email);
    }
}
