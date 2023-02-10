namespace WebApi.BE;

public interface IUserResponse : IUserModel
{
    int Id { get; set; }
    DateTime CreatedAt { get; set; }
}