using LoginWithoutIDCore.Data;
using LoginWithoutIDCore.Models.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LoginWithoutIDCore.CustomAuth
{
    public class UniqueSessionRequirement : AuthorizationHandler<UniqueSessionRequirement>, IAuthorizationRequirement
    {
        private readonly LoginDbContext dbContext;
        public UniqueSessionRequirement(LoginDbContext DbContext)
        {
            dbContext = DbContext;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, UniqueSessionRequirement requirement)
        {
            var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier);
            var securityTokenClaim = context.User.FindFirst("SecurityToken");

            if (userIdClaim == null)
            {
                context.Fail();
                return;
            }

            var userId = int.Parse(userIdClaim.Value);
            var securityToken = securityTokenClaim.Value;
            var user = await GetUserFromDatabase(userId);

            if (user == null || user.TokenStatus)
            {
                context.Fail();
                return;
            }
            context.Succeed(requirement);
            return;
        }
        private async Task<User> GetUserFromDatabase(int userId)
        {
            // Implement your database query logic here
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
            return user;
        }
    }
}
