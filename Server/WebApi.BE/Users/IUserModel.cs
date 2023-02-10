namespace WebApi.BE;

public interface IUserModel
{
    string LastName { get; set; }
    string FirstName { get; set; }
    string? MiddleName { get; set; }
    string? Email { get; set; }
    string Login { get; set; }
}