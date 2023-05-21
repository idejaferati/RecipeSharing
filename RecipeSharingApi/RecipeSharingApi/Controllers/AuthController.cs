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

        [HttpGet, Authorize(Policy="onlyadmin")]
        public ActionResult<string> GetMyName()
        {
            return Ok(_userService.GetMyId());

        }


        [HttpPost("login")]
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

            //return Ok(token);
            return Ok(new { Token = token });
        }

        [HttpPost("addrole")]
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


        [HttpGet("getRoles")]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {
            var roles = await _context.Roles.ToListAsync();
            return Ok(roles);
        }


        [HttpGet("getPolicies")]
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
