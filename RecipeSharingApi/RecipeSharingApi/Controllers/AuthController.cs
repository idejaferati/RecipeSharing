using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RecipeSharingApi.BusinessLogic.Services;
using RecipeSharingApi.DataLayer.Data;
using RecipeSharingApi.DataLayer.Models.DTOs;
using RecipeSharingApi.DataLayer.Models.Entities;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using RecipeSharingApi.DataLayer.Models.DTOs.User;
using Microsoft.EntityFrameworkCore;

namespace RecipeSharingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {

        public static UserCreateDTO user = new UserCreateDTO();
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly RecipeSharingDbContext _context;

        public AuthController(IConfiguration configuration, IUserService userService, RecipeSharingDbContext context)
        {
            _configuration = configuration;
            _userService = userService;
            _context = context;
        }

        [HttpGet, Authorize(Roles = "admin")]
        public ActionResult<string> GetMyName()
        {
            return Ok(_userService.GetMyName());

            //var userName = User?.Identity?.Name;
            //var roleClaims = User?.FindAll(ClaimTypes.Role);
            //var roles = roleClaims?.Select(c => c.Value).ToList();
            //var roles2 = User?.Claims
            //    .Where(c => c.Type == ClaimTypes.Role)
            //    .Select(c => c.Value)
            //    .ToList();
            //return Ok(new { userName, roles, roles2 });
        }

<<<<<<< Updated upstream

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserLoginDto dto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
            {
                return BadRequest("Username is already taken.");
            }

            var salt = BCrypt.Net.BCrypt.GenerateSalt();
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.password, salt);


            var user = new UserCreateDTO
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Gender = dto.Gender,
                DateOfBirth = dto.DateOfBirth,
                Email = dto.Email,
                Roles = dto.Roles,
                PhoneNumber = dto.PhoneNumber,
                SaltedHashPassword =passwordHash,
                Salt =salt

            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("User created successfully.");
        }

        

=======
>>>>>>> Stashed changes
        [HttpPost("login")]
        public async Task<IActionResult> Login(string email, string roles, string password)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return BadRequest("Invalid username.");
            }
            if (roles != user.Roles)
            {
                return BadRequest("Invalid role.");
            }

            var saltedPasswordHash = BCrypt.Net.BCrypt.HashPassword(password, user.Salt);
            if (saltedPasswordHash != user.SaltedHashPassword)
            {
                return BadRequest("Invalid password.");
            }

            string token = CreateToken(user);

            return Ok(token);
        }


        

<<<<<<< Updated upstream
        private string CreateToken(UserCreateDTO user)
=======


            return Ok("U regjsitrua me sukses");
        }

        [HttpPost("addPolicy")]
        public async Task<ActionResult<Policy>> CreatePolicy(string name)
        {

            var policy1 = new Policy
            {
                Name = name
            };

            _context.Policies.Add(policy1);
            await _context.SaveChangesAsync();



            return Ok("U regjsitrua me sukses");
        }

        [HttpPost("addRolePolicy")]
        public async Task<ActionResult<PolicyRole>> CreatePolicyRole(Guid policyId ,Guid roleId)
        {

            var policyRole1 = new PolicyRole
            {
                PolicyId = policyId,
                RoleId = roleId
            };

            _context.PolicyRoles.Add(policyRole1);
            await _context.SaveChangesAsync();



            return Ok("U regjsitrdua me sukses");
        }
        private string CreateToken(User user)
>>>>>>> Stashed changes
        {
            List<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Roles)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }


    }
}
