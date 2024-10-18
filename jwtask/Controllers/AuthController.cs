using jwtask.models;
using jwtask.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace jwtask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUser _user;
        private readonly IConfiguration _configuration;
        public AuthController(IUser user, IConfiguration config)
        {
            _user = user;
            _configuration = config;
        }

        [HttpPost("register")]
        public IActionResult register(User user)
        {
            _user.Register(user);
            return Ok();
        }
        [HttpPost("login")]
        public IActionResult Login(Login user)
        {
            try
            {
                var u = _user.Login(user);
                if (user == null)
                {
                    return BadRequest("Invald username or password");
                }
                string token = CreateToken(u);
                return Ok(token);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server eroor"+ex);
            }
        }

        [Authorize]
        [HttpGet("getuser")]
        public IActionResult GetUser()
        {
            return Ok(_user.GetUsers());
        }

        private string CreateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim (ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim (ClaimTypes.Name,user.UserName),
                new Claim (ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(
                    claims: claims,
                    signingCredentials: credentials,
                    expires: DateTime.Now.AddDays(1)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public List<int> numbers = new List<int>
        {
            1,2, 3, 4, 5, 6, 7, 8, 9, 10
        };

        [HttpGet("{id}")]
        public ActionResult GetNumbers(int id)
        {
            var num = numbers.FirstOrDefault(x=>x==id);

            if(num != 0)
            {
                return Ok(hello());
            }
            else
            {
                return BadRequest();
            }

        }

        [NonAction]
        public string hello()
        {
            return "hello world";
        }


    }
}
