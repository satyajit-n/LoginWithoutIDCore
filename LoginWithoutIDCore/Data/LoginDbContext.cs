using LoginWithoutIDCore.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace LoginWithoutIDCore.Data
{
    public class LoginDbContext: DbContext
    {
        public LoginDbContext(DbContextOptions<LoginDbContext> dbContextOptions) : base(dbContextOptions)
        {
            
        }
        public virtual DbSet<User> Menus { get; set; }
    }
}
