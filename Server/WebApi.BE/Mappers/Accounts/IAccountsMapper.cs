using System.Data;
using System.Data.SqlClient;
using WebApi.BE.Account;
namespace WebApi.BE.Mappers
{
    public interface IAccountsMapper
    {
        AccountInfoModel MapReaderToAccountInfoModel(SqlDataReader reader);

        AccountInfoSectionAccessModel MapReaderToAccountInfoSectionAccessModel(SqlDataReader reader);

        AccountInfoSectionRelationAccount MapReaderToAccountInfoSectionRelationAccountModel(SqlDataReader reader);
    }
}
