using System.Data.SqlClient;

namespace WebApi.BE.Mappers;

public interface ISectionMapper
{
    SectionModel MapReaderToSectionModel(SqlDataReader reader);
}