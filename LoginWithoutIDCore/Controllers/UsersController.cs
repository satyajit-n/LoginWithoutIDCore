using AutoMapper;
using LoginWithoutIDCore.Data;
using LoginWithoutIDCore.Models.Domain;
using LoginWithoutIDCore.Models.Dto;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LoginWithoutIDCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly LoginDbContext _context;
        private readonly IMapper mapper;

        public UsersController(LoginDbContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/Users
        [HttpGet]
        [Authorize(Roles = "string")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var userSessionId = HttpContext.Session.GetString(SessionVariables.SessionKeySessionId);
            var userCookieId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userCookieId.Value != userSessionId)
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return BadRequest("Session Expired");
            }
            if (_context.Users == null)
            {
                return NotFound();
            }
            var UserList = await _context.Users.ToListAsync();
            return Ok(UserList);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        [Authorize(Roles = "string")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "string")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "string")]
        public async Task<ActionResult<User>> PostUser(AddUserDto addUserDto)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'LoginDbContext.Users'  is null.");
            }
            var userDomain = mapper.Map<User>(addUserDto);
            _context.Users.Add(userDomain);
            await _context.SaveChangesAsync();


            return CreatedAtAction("GetUser", new { id = userDomain.Id }, userDomain);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "string")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
