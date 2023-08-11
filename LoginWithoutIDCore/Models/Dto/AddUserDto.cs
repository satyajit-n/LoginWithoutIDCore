using System.ComponentModel.DataAnnotations;

namespace LoginWithoutIDCore.Models.Dto
{
    public class AddUserDto
    {
        public required string Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public required string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
        public required string Roles { get; set; }
    }
}
