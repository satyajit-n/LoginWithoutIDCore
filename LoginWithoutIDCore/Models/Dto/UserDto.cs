namespace LoginWithoutIDCore.Models.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string Roles { get; set; }
    }
}
