using WebApi.BE.Account;

namespace WebApi.DAL
{
    public interface IAccountsRepository
    {
        Task<List<AccountInfoModel>> GetAll(string? term);

        Task<AccountInfoModel> GetByGuid(Guid guid);

        Task<Guid> DeleteByGuid (Guid guid);

        Task<int> GetAccountsCountAll();
    }
}
