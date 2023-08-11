using LoginWithoutIDCore.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace LoginWithoutIDCore.Data
{
    public class LoginDbContext: DbContext
    {
        public LoginDbContext(DbContextOptions<LoginDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
        }
    }
}
