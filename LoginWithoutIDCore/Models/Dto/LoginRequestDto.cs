using System.ComponentModel.DataAnnotations;

namespace LoginWithoutIDCore.Models.Dto
{
    public class LoginRequestDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public required string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}
