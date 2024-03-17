using LoginApp.API.Application.DTOs;
using LoginApp.API.Infrastructure.Services;
using LoginApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LoginApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly LoginDbContext _context;

        public UsersController(LoginDbContext context)
        {
            _context = context;
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> PostAsync(User user)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
            if (existingUser != null)
            {
                return BadRequest(new
                {
                    message = "User Already Exist",
                    User = user
                });
            }

            try
            {
                user.MemberSince = DateTime.Now;
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

                var response = new
                {
                    message = "User Created Successfully",
                    User = user
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.ToString() });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginInputDto user)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email && u.Pwd == user.Pwd);
            if (existingUser == null)
            {
                return BadRequest(new { message = "Failure"});
            }
            var token = GenerateJwtToken(existingUser);
            return Ok(new { token });
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = SecurityHelper.GenerateRandomKey(32);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
          {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()), // User ID
            new Claim(ClaimTypes.Email, user.Email), // Email
            new Claim(ClaimTypes.GivenName, user.FirstName), // First name
            new Claim(ClaimTypes.Surname, user.LastName), // Last name
            new Claim(ClaimTypes.MobilePhone, user.Mobile), // Mobile number
            new Claim(ClaimTypes.Gender, user.Gender ? "Male" : "Female"), // Gender
            new Claim(ClaimTypes.Role, "User"), // Assuming all users have the "User" role
                                                // You can add more claims here if needed
          }),
                Expires = DateTime.UtcNow.AddHours(1), // Token expires after 1 hour, adjust as needed
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}