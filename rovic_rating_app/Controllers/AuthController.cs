using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using rovic_rating_app.Configurations;
using rovic_rating_app.Models.DTOs;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace rovic_rating_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly UserManager<IdentityUser<int>> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly JwtConfig _jwtConfig;

        public AuthController(UserManager<IdentityUser<int>> userManager, IOptionsMonitor<JwtConfig> optionsMonitor, RoleManager<IdentityRole<int>> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtConfig = optionsMonitor.CurrentValue;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDTO register)
        {
            if (ModelState.IsValid)
            {
                var emailExist = await _userManager.FindByEmailAsync(register.Email);

                if(emailExist != null)
                    return BadRequest("Email already exist");

                var newUser = new IdentityUser<int>
                {
                    Email = register.Email,
                    UserName = register.Name
                };

                var isCreated = await _userManager.CreateAsync(newUser, register.Password);

                if (isCreated.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newUser, "User");

                    return Ok(new UserRegistrationResponseDTO()
                    {
                        Result = true,
                        Token = await GenerateJwtToken(newUser)
                    });
                }

                return BadRequest(isCreated.Errors.Select(x => x.Description).ToList());
            }

            return BadRequest("Invalid request");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO login)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(login.Email);

                if(existingUser == null)
                {
                    return BadRequest("Invalid authentication");
                }

                var isPasswordValid = await _userManager.CheckPasswordAsync(existingUser, login.Password);

                if(isPasswordValid)
                {
                    var token = await GenerateJwtToken(existingUser);

                    return Ok(new UserLoginResponseDTO()
                    {
                        Token = token,
                        Result = true
                    });
                }

                return BadRequest("Invalid authentication");
            }

            return BadRequest("Invalid request");
        }

        private async Task<string> GenerateJwtToken(IdentityUser<int> user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            var claims = await GetAllValidClaims(user);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(4),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }

        private async Task<List<Claim>> GetAllValidClaims(IdentityUser<int> user)
        {
            var options = new IdentityOptions();

            var claims = new List<Claim>
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);

            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));

                var role = await _roleManager.FindByNameAsync(userRole);

                if(role != null)
                {
                    var roleClaims = await _roleManager.GetClaimsAsync(role);

                    foreach (var roleClaim in roleClaims)
                    {
                        claims.Add(roleClaim);
                    }
                }
            }

            return claims;
        }
    }
}
