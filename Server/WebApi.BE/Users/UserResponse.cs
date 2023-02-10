namespace WebApi.BE;

public class UserResponse : IUserResponse
{
    public int Id { get; set; }
    public Guid? Guid { get; set; }
    public string LastName { get; set; } = "";
    public string FirstName { get; set; } = "";
    public string? MiddleName { get; set; } = null;
    public string? Email { get; set; } = null;
    public string Login { get; set; } = "";
    public DateTime CreatedAt { get; set; }
}