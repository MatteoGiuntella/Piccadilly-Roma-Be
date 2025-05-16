using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Piccadilly_Roma_Be.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Piccadilly_Roma_Be.Data;
using Org.BouncyCastle.Crypto.Generators;


namespace Piccadilly_Roma_Be.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly RistoranteContext ristoranteContext;
        private readonly IConfiguration _config;

        // Simulazione in memoria (puoi sostituire con DB)
        private static readonly Admin AdminFake = new Admin
        {
            Username = "admin",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123")
        };

        public AuthController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Admin login)
        {

            if ( login.Username != AdminFake.Username ||
                !BCrypt.Net.BCrypt.Verify(login.PasswordHash, AdminFake.PasswordHash)  )
            {
                return Unauthorized("Credenziali non valide");
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, login.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddHours(3),
                claims: claims,
                signingCredentials: creds);

            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
    }
}