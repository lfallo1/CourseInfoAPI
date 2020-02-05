using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using CourseInfoAPI.DbContexts;
using CourseInfoAPI.Entities;
using CourseInfoAPI.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using static BCrypt.Net.BCrypt;

namespace CourseInfoAPI.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConfiguration _configuration;

        private readonly CourseLibraryContext _context;

        public AuthenticationService(IConfiguration configuration, CourseLibraryContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public User AuthenticateUser(LoginDto login)
        {
            User user = GetUser(login.Username);
            if(null != user && Verify(login.Password, user.Password))
            {
                return user;
            }
            return null;
        }

        public User GetUser(string username)
        {
            return _context.Users.Where(u => u.Username == username).First();
        }

        public string GenerateJsonWebToken(User user)
        {
            var jwtKey = _configuration["Jwt:Key"];
            var jwtIssuer = _configuration["Jwt:Issuer"];

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtIssuer,
                claims: claims,
                expires: DateTime.Now.AddMinutes(2),
                signingCredentials: credentials);

            var tokenForReturn = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenForReturn;
        }
    }
}
