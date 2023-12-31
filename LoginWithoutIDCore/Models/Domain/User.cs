﻿using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations;

namespace LoginWithoutIDCore.Models.Domain
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string Roles { get; set; }
        public Boolean TokenStatus { get; set; }
    }
    
}
