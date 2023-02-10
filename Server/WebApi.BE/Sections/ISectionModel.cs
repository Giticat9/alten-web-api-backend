namespace WebApi.BE;

public interface ISectionModel
{
    long Id { get; set; }
    Guid Guid { get; set; }
    string Name { get; set; }
    string Description { get; set; }
}