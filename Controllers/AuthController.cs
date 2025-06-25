using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web.Http;
using LibraryManagement.Domain;
using Microsoft.IdentityModel.Tokens;

namespace LibraryManagement.Controllers
{
    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {
        // Temporary in-memory user store for demonstration
        private static List<User> _users = new List<User>();
        private static int _nextId = 1;

        // POST: api/auth/register
        [HttpPost]
        [Route("register")]
        public IHttpActionResult Register(User model)
        {
            if (model == null || string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
                return BadRequest("Username and password are required.");

            if (_users.Any(u => u.Username == model.Username))
                return BadRequest("Username already exists.");

            model.Id = _nextId++;
            _users.Add(model);
            return Ok("User registered successfully.");
        }

        // POST: api/auth/login
        [HttpPost]
        [Route("login")]
        public IHttpActionResult Login(User credentials)
        {
            if (credentials == null)
                return BadRequest("Invalid login request.");

            var user = _users.FirstOrDefault(u => 
                u.Username == credentials.Username && 
                u.Password == credentials.Password);
            if (user == null)
                return Unauthorized();

            var token = GenerateJwtToken(user);
            return Ok(new { token });
        }

        // Minimal token generation logic
        private string GenerateJwtToken(User user)
        {
            // Replace with a secure key from config or secrets for production
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("a-very-long-and-secure-key-1234567890"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim("userId", user.Id.ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: "LibraryAuthServer",
                audience: "LibraryAuthClient",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}