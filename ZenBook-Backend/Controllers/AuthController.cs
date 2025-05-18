using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ZenBook_Backend.Models;


namespace ZenBook_Backend.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _users;
        private readonly IConfiguration _config;

        public AuthController(UserManager<ApplicationUser> users, IConfiguration config)
        {
            _users = users;
            _config = config;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            // Assume dto has Username, Email, Password, TenantId
            var user = new ApplicationUser
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                UserName = dto.Username,
                Email = dto.Email,
                EmailConfirmed = dto.EmailConfirmed,
                PhoneNumberConfirmed = dto.PhoneNumberConfirmed,
                TwoFactorEnabled = dto.TwoFactorEnabled,
                LockoutEnabled = dto.LockoutEnabled,
                AccessFailedCount = dto.AccessFailedCount,
                TenantId = dto.TenantId,

            };
            var res = await _users.CreateAsync(user, dto.Password);
            if (!res.Succeeded) return BadRequest(res.Errors);

            return Ok();
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _users.FindByNameAsync(dto.Username);
            if (user == null || !await _users.CheckPasswordAsync(user, dto.Password))
                return Unauthorized("Invalid creds");

            // Optionally check tenant:
            if (user.TenantId != dto.TenantId)
                return Unauthorized("Wrong tenant");

            var claims = new List<Claim> {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim("tenant", user.TenantId),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var jwt = _config.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(double.Parse(jwt["DurationMinutes"]!));

            var token = new JwtSecurityToken(
                issuer: jwt["Issuer"],
                audience: jwt["Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expires = expires
            });
        }
    }

    public record LoginDto(string Username, string Password, string TenantId);
    public record RegisterDto(string FirstName, string LastName, string Username, string Email, string Password, string TenantId, bool EmailConfirmed, bool PhoneNumberConfirmed, bool TwoFactorEnabled, bool LockoutEnabled, int AccessFailedCount);




}