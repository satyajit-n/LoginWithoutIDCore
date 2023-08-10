using System.ComponentModel.DataAnnotations;

namespace LoginWithoutIDCore.Models.Domain
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string City { get; set; }

    }
}
