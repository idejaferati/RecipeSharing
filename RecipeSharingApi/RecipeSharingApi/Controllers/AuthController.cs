using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RecipeSharingApi.DataLayer.Data;
using RecipeSharingApi.DataLayer.Models.DTOs;
using RecipeSharingApi.DataLayer.Models.Entities;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using RecipeSharingApi.DataLayer.Models.DTOs.User;
using Microsoft.EntityFrameworkCore;
using RecipeSharingApi.DataLayer.Models.Entities.Mappings;
using RecipeSharingApi.BusinessLogic.Services;

namespace RecipeSharingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {

        //public static User user = new User();
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly RecipeSharingDbContext _context;

        public AuthController(IConfiguration configuration, IUserService userService, RecipeSharingDbContext context)
        {
            _configuration = configuration;
            _userService = userService;
            _context = context;
        }

        /// <summary>
        /// Retrieves the name of the authenticated user.
        /// </summary>
        /// <returns>The name of the authenticated user.</returns>
        [HttpGet]
        [Authorize(Policy = "adminPolicy")]
        [ProducesResponseType(typeof(string), 200)]
        public ActionResult<string> GetMyName()
        {
            return Ok(_userService.GetMyId());

        }


        /// <summary>
        /// Authenticates a user and returns an access token.
        /// </summary>
        /// <param name="dto">The user login data.</param>
        /// <returns>An access token.</returns>
        [HttpPost("login")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> Login(UserForLoginDto dto)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null)
            {
                return BadRequest("Invalid username.");
            }

            var saltedPasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password, user.Salt);
            if (saltedPasswordHash != user.SaltedHashPassword)
            {
                return BadRequest("Invalid password.");
            }
            
            string token = CreateToken(user);

            return Ok(new { Token = token });
        }

        /// <summary>
        /// Creates a new role.
        /// </summary>
        /// <param name="name">The name of the role.</param>
        /// <returns>A message indicating the result of the creation.</returns>
        [HttpPost("addrole")]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<ActionResult<Role>> CreateRole(string name)
        {

            var role1 = new Role
            {
                Name = name
            };
            _context.Roles.Add(role1);
            await _context.SaveChangesAsync();



            return Ok("U regjsitrua me sukses");
        }

        /// <summary>
        /// Creates a new policy.
        /// </summary>
        /// <param name="name">The name of the policy.</param>
        /// <returns>A message indicating the result of the creation.</returns>
        [HttpPost("addPolicy")]
        [ProducesResponseType(typeof(string), 200)]
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

        /// <summary>
        /// Creates a mapping between a policy and a role.
        /// </summary>
        /// <param name="policyId">The ID of the policy.</param>
        /// <param name="roleId">The ID of the role.</param>
        /// <returns>A message indicating the result of the creation.</returns>
        [HttpPost("addRolePolicy")]
        [ProducesResponseType(typeof(string), 200)]
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

        /// <summary>
        /// Retrieves all roles.
        /// </summary>
        /// <returns>A list of roles.</returns>
        [HttpGet("getRoles")]
        [ProducesResponseType(typeof(List<Role>), 200)]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {
            var roles = await _context.Roles.ToListAsync();
            return Ok(roles);
        }


        /// <summary>
        /// Retrieves all policies.
        /// </summary>
        /// <returns>A list of policies.</returns>
        [HttpGet("getPolicies")]
        [ProducesResponseType(typeof(List<Policy>), 200)]
        public async Task<ActionResult<IEnumerable<Policy>>> GetPolicies()
        {
            var policies = await _context.Policies.ToListAsync();
            return Ok(policies);
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.RoleId.ToString())
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
