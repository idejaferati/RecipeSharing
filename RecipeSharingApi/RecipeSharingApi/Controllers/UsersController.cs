using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RecipeSharingApi.BusinessLogic.Services;
using RecipeSharingApi.DataLayer.Data;
using RecipeSharingApi.DataLayer.Models.DTOs.User;
using RecipeSharingApi.DataLayer.Models.Entities;
using System.Security.Claims;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Adapters;
using Newtonsoft.Json.Linq;

namespace RecipeSharingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly RecipeSharingDbContext _context;
        public UsersController( RecipeSharingDbContext context)
        {
            _context = context;
        }


        [HttpPost("register")]

        public async Task<IActionResult> Register(UserRegisterDto dto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
            {
                return BadRequest("Username is already taken.");
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
        [Authorize(Policy = "onlyadmin")]
        public async Task<IActionResult> DeleteUser(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok("u fshi me sukses");
        }

        [HttpGet("findAllusers")]
        [Authorize(Policy = "onlyadmin")]
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
        [Authorize(Policy = "onlyadmin")]
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
        [Authorize(Policy = "onlyadmin")]

        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Change password data is null.");
            }

            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (email == null)
            {
                return Unauthorized();
            }

            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
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

            //CreatePasswordHash(dto.NewPassword, out byte[] passwordHash, out byte[] passwordSalt);
            user.SaltedHashPassword = passwordHash;
            user.Salt = salt;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("my-data")]
        [Authorize(Policy = "onlyadmin")]
        public async Task<ActionResult<User>> GetUser()
        {

            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (email == null)
            {
                return Unauthorized("pa autorizun");
            }
            // Get the current user's ID from the authentication token

            var currentUserEmail = await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
            if (currentUserEmail == null)
            {
                return NotFound();
            }

            // Retrieve the user from the database
            var user = await _context.Users
                                       .Where(u => u.Email == email)
                                       .Select(u => new {
                                                          FirstName = u.FirstName,
                                                          LastName = u.LastName,
                                                          Gender = u.Gender,
                                                          DateOfBirth = u.DateOfBirth,
                                                          PhoneNumber = u.PhoneNumber
                                       })
                                       .FirstOrDefaultAsync();


            // Check if the user exists
            if (user == null)
            {
                return NotFound();
            }

            // Return the user data
            return Ok(user);
        }


    }
}
