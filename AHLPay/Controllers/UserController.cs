using Domain.DTOs;
using Domain.Models;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace AHLPay.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly AhlDbContext _context;

        public UserController(AhlDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterUserDto registerUser)
        {
            var user = new User
            {
                Username = registerUser.Username,
                Email = registerUser.Email,
                PasswordHash = HashPassword(registerUser.Password)
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok(new { UserId = user.Id, Username = user.Username, Email = user.Email });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginUserDto loginUser)
        {
            var user = _context.Users.SingleOrDefault(u => u.Username == loginUser.Username);

            if (user == null || !VerifyPassword(loginUser.Password, user.PasswordHash))
            {
                return Unauthorized();
            }

            return Ok();
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            var hashedInputPassword = HashPassword(password);
            return hashedInputPassword == hashedPassword;
        }
    }
}
