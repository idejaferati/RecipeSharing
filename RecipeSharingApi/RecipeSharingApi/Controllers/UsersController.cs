using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeSharingApi.BusinessLogic.Services;
using RecipeSharingApi.DataLayer.Data;
using RecipeSharingApi.DataLayer.Models.DTOs.User;
using RecipeSharingApi.DataLayer.Models.Entities;

namespace RecipeSharingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly RecipeSharingDbContext _context;
        private readonly IUserService _userService;
        public UsersController(RecipeSharingDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }


        [HttpPost("register")]

        public async Task<IActionResult> Register(UserRegisterDto dto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
            {
                return BadRequest("Email is already taken.");
            }

            var salt = BCrypt.Net.BCrypt.GenerateSalt();
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.password, salt);


            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Gender = dto.Gender,
                DateOfBirth = dto.DateOfBirth,
                Email = dto.Email,
                RoleId = dto.RoleId,
                PhoneNumber = dto.PhoneNumber,
                SaltedHashPassword = passwordHash,
                Salt = salt

            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("User created successfully.");
        }



        [HttpDelete("{email}")]
        [Authorize(Policy = "adminPolicy")]
        public async Task<IActionResult> DeleteUser(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok("User Deleted Successfully");
        }

        [HttpGet("findAllusers")]
        [Authorize(Policy = "adminPolicy")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();

            if (users == null || users.Count == 0)
            {
                return NoContent();
            }

            return Ok(users);
        }

        [HttpGet("search-by-email/{email}")]
        [Authorize(Policy = "adminPolicy")]
        public async Task<ActionResult<IEnumerable<User>>> SearchUsersByUsername(string email)
        {
            var users = await _context.Users.Where(u => u.Email.Contains(email)).ToListAsync();

            if (users == null || users.Count == 0)
            {
                return NoContent();
            }

            return Ok(users);
        }


        [HttpPatch("change-password")]
        [Authorize(Policy = "userPolicy")]

        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Change password data is null.");
            }

            var userID = _userService.GetMyId();
            if (userID == null)
            {
                return Unauthorized();
            }

            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == userID);
            if (user == null)
            {
                return NotFound();
            }

            var saltedPasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.OldPassword, user.Salt);
            if (saltedPasswordHash != user.SaltedHashPassword)
            {
                return BadRequest("Invalid password.");
            }
            //if (!VerifyPasswordHash(dto.OldPassword, user.SaltedHashPassword, user.Salt))
            //{
            //    return BadRequest("Old password is incorrect.");
            //}

            var salt = BCrypt.Net.BCrypt.GenerateSalt();
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword, salt);

            user.SaltedHashPassword = passwordHash;
            user.Salt = salt;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("my-data")]
        [Authorize(Policy = "userPolicy")]
        public async Task<ActionResult<User>> GetUser()
        {

            var userID = _userService.GetMyId();
            if (userID == null)
            {
                return Unauthorized("Not authorized to get data for user");
            }

            var currentUserEmail = await _context.Users.SingleOrDefaultAsync(u => u.Id == userID);
            if (currentUserEmail == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                                       .Where(u => u.Id == userID)
                                       .Select(u => new
                                       {
                                           Id = u.Id,
                                           FirstName = u.FirstName,
                                           LastName = u.LastName,
                                           Email = u.Email,
                                           Gender = u.Gender,
                                           DateOfBirth = u.DateOfBirth,
                                           PhoneNumber = u.PhoneNumber
                                       })
                                       .FirstOrDefaultAsync();


            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
    }
}
