using DAL;
using API.Filters.ActionFilters;
using API.Helpers;
using MODELS;
using SERVICES.Interfaces;
using IDCraftSolutionWebAPI.Models.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [SkipUserContextFilter]
    public class LoginController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;
        private readonly ILogger<LoginController> _logger;
        private readonly IUserDetailsService _userDetailsService;
        public LoginController(AppDbContext context, IConfiguration configuration, ILogger<LoginController> logger,
            IUserDetailsService userDetailsService)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
            _userDetailsService = userDetailsService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            try
            {
                // Sample hardcoded login - replace with real DB check
                var user = await _context.UserDetails.FirstOrDefaultAsync(u => u.EmailId == login.Username && !u.IsDeleted);

                if (user == null)
                    return ErrorResponse("Invalid username or password.");

                // For real apps, hash the input password and compare hashes
                string encrypted = CryptoHelper.Encrypt(login.Password);
                if (user?.Password != encrypted)
                    return ErrorResponse("Invalid username or password.");

                var userRole = await _context.Roles.FirstOrDefaultAsync(u => user.RoleId == u.Id && !u.IsDeleted);
                var claims = new[]
                    {
                    new Claim(ClaimTypes.Email, user.EmailId),
                    new Claim(ClaimTypes.Name, user.EmailId),
                    new Claim("emailid", user.EmailId),
                    new Claim("name", user.Name),
                    new Claim("role", userRole?.RoleName?.ToLower()?.ToString()??""),
                    new Claim("userid", user.Id.ToString()),
                };
                var jwtSettings = _configuration.GetSection("JwtSettings");
                var secretKey = jwtSettings["SecretKey"];

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: jwtSettings["Issuer"],
                    audience: jwtSettings["Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddHours(24),
                    signingCredentials: creds);

                return Response(new JwtSecurityTokenHandler().WriteToken(token));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ErrorResponse("Something went wrong, Please try again later.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostUserDetail(UserDetail userDetail)
        {
            try
            {
                string encrypted = CryptoHelper.Encrypt(userDetail.Password);
                userDetail.Password = encrypted;

                var userExists = await _context.UserDetails.FirstOrDefaultAsync(r => r.EmailId == userDetail.EmailId && !r.IsDeleted);
                if (userExists != null)
                {
                    return ErrorResponse("A user with this email address already exists. Try different email address to register.");
                }

                userDetail.RoleId = (int)UserRoles.User;
                userDetail.CreatedOn = DateTime.UtcNow;
                userDetail.CreatedBy = "System";
                _context.UserDetails.Add(userDetail);
                var result = await _context.SaveChangesAsync();

                if (result <= 0)
                {
                    return ErrorResponse("Failed to create user. Please try again later.");
                }
                return Response("User created successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ErrorResponse("Something went wrong, Please try again later.");
            }
        }
    }
}
