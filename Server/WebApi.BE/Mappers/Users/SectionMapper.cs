using System.Data;
using System.Data.SqlClient;

namespace WebApi.BE.Mappers;

public class SectionMapper : ISectionMapper
{
    public SectionModel MapReaderToSectionModel(SqlDataReader reader)
    {
        return new SectionModel
        {
            Id = (int)reader.GetValue("id"),
            Guid = (Guid)reader.GetGuid("guid"),
            Name = (string)reader.GetValue("name"),
            Description = (string)reader.GetValue("description")
        };
    }
}