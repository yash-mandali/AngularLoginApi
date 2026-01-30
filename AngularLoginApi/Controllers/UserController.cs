using AngularLoginApi.Data;
using AngularLoginApi.Model;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AngularLoginApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly GetJwtToken _getToken;
        private readonly IConfiguration _configuration;
       
        public UserController(AppDbContext context, IConfiguration configuration, GetJwtToken getToken)
        {
            _context = context;
            _configuration = configuration;
            _getToken = getToken;
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IEnumerable<User>> GetAllUsers() {
            return await _context.Users.ToListAsync();
        }

        [HttpPost]
        [Route("signup")]
        public async Task<IActionResult> Signup(User users)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
            var user = _context.Users.Where(u => (u.Email == users.Email)).FirstOrDefault();
                if (user != null)
                {
                    return Conflict(new { message = "User already exists" });
                }

                users.Password = BCrypt.Net.BCrypt.HashPassword(users.Password);

                _context.Users.Add(users);
                await _context.SaveChangesAsync();
                return Ok(new { message = "signup successfull" });    
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Internal server error" });
            }
            
        }
            
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(Login users)
        {
            if (!ModelState.IsValid)
            {   
                return BadRequest(ModelState);
            }
            try 
            {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == users.Email);
                if (user == null)
                {
                    return Unauthorized(new { message = "Invalid email"});
                }

                if(user.Role != users.Role)
                {
                    return Unauthorized(new { message = "Authentication failed" });
                }   

                //----------- or ----------------

                //if (!string.Equals(user.Role, users.Role, StringComparison.OrdinalIgnoreCase))
                //{
                //    return Unauthorized(new { message = "Authentication failed" });
                //}

                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(users.Password, user.Password);

                if (!isPasswordValid)
                {
                    return Unauthorized(new { message = "Invalid  password" });

                }
                var Token = _getToken.GenerateJwtToken(users);

                return Ok(new { message = "Login successfull",token = Token});
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Internal server error" });
            }
        }
    }
}
