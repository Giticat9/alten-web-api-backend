namespace WebApi.BE
{
    public class UserSearchModel : IUserSearchModel
    {
        public int? Id { get; set; } = null;
        public Guid? Guid { get; set; } = null;
        public string? Email { get; set; } = null;
        public string? Login { get; set; } = null;
    }
}