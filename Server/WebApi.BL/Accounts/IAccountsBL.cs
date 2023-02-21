using WebApi.BE.Account;

namespace WebApi.BL
{
    public interface IAccountsBL
    {
        Task<List<AccountInfoModel>> GetAll(string? term = null);

        Task<AccountInfoModel> GetByGuid(Guid guid);

        Task<Guid> DeleteByGuid (Guid guid);

        Task<int> GetAccountsCountAll();
    }
}
