namespace WebApi.BE
{
    public interface IUserSearchModel
    {
        int? Id { get; set; }
        Guid? Guid { get; set; }
        string? Email { get; set; }
        string? Login { get; set; }
    }
}