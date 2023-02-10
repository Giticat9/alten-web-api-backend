namespace WebApi.BE;

public class SectionModel : ISectionModel
{
    public long Id { get; set; } = 0;
    public Guid Guid { get; set; } = Guid.Empty;
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
}