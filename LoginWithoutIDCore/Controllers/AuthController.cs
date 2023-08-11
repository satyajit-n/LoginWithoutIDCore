using AutoMapper;
using LoginWithoutIDCore.Data;
using LoginWithoutIDCore.Models.Dto;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LoginWithoutIDCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly LoginDbContext _dbContext;
        private readonly IMapper mapper;

        public AuthController(LoginDbContext DbContext,IMapper mapper)
        {
            _dbContext = DbContext;
            this.mapper = mapper;
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == loginRequestDto.Email);

            if (user != null)
            {
                if (!user.TokenStatus)
                {
                    if (user.Password == loginRequestDto.Password)
                    {
                        user.TokenStatus = true;
                        await _dbContext.SaveChangesAsync();
                        var Claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name,user.Name),
                        new Claim(ClaimTypes.Email,user.Email),
                        new Claim(ClaimTypes.Role,user.Roles),
                        new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                    };
                        var claimsIdentity = new ClaimsIdentity(Claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        await HttpContext.SignInAsync(
                                    CookieAuthenticationDefaults.AuthenticationScheme,
                                    new ClaimsPrincipal(claimsIdentity));
                        return Ok(new
                        {
                            user.Email,
                            user.Name,
                            user.Roles
                        });
                    } 
                }
                return BadRequest(new {Message = "You are already logged in"});
            }
            return Unauthorized();

        }

        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == int.Parse(userIdClaim.Value));
            if (user != null)
            {
                user.TokenStatus = false;
                await _dbContext.SaveChangesAsync();
            }

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Ok(new { message = "Logout successful" });
        }
    }
}
