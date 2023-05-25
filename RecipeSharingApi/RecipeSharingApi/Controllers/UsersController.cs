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


        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="dto">The user registration data.</param>
        /// <returns>A message indicating the result of the registration.</returns>
        [HttpPost("register")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 400)]
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



        /// <summary>
        /// Deletes a user by email.
        /// </summary>
        /// <param name="email">The email of the user to delete.</param>
        /// <returns>A message indicating the result of the deletion.</returns>
        [HttpDelete("{email}")]
        [Authorize(Policy = "adminPolicy")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 404)]
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

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns>A list of users.</returns>
        [HttpGet("findAllusers")]
        [Authorize(Policy = "adminPolicy")]
        [ProducesResponseType(typeof(List<User>), 200)]
        [ProducesResponseType(typeof(string), 204)]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();

            if (users == null || users.Count == 0)
            {
                return NoContent();
            }

            return Ok(users);
        }

        /// <summary>
        /// Searches for users by email.
        /// </summary>
        /// <param name="email">The email to search for.</param>
        /// <returns>A list of matching users.</returns>
        [HttpGet("search-by-email/{email}")]
        [Authorize(Policy = "adminPolicy")]
        [ProducesResponseType(typeof(List<User>), 200)]
        [ProducesResponseType(typeof(string), 204)]
        public async Task<ActionResult<IEnumerable<User>>> SearchUsersByUsername(string email)
        {
            var users = await _context.Users.Where(u => u.Email.Contains(email)).ToListAsync();

            if (users == null || users.Count == 0)
            {
                return NoContent();
            }

            return Ok(users);
        }


        /// <summary>
        /// Changes the password for the currently authenticated user.
        /// </summary>
        /// <param name="dto">The change password data.</param>
        /// <returns>A message indicating the result of the password change.</returns>
        [HttpPatch("change-password")]
        [Authorize(Policy = "userPolicy")]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 401)]
        [ProducesResponseType(typeof(string), 404)]
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

        /// <summary>
        /// Retrieves the data of the currently authenticated user.
        /// </summary>
        /// <returns>The user data.</returns>
        [HttpGet("my-data")]
        [Authorize(Policy = "userPolicy")]
        [ProducesResponseType(typeof(User), 200)]
        [ProducesResponseType(typeof(string), 401)]
        [ProducesResponseType(typeof(string), 404)]
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
