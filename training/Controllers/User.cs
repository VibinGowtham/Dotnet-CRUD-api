using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using training.Model;

namespace training.Controllers
{
    public class Usercontroller : ControllerBase
    {
        private readonly studentDbContext _studentDbContext;

        private readonly ILogger<Usercontroller> _logger;

        private readonly IConfiguration _configuration;

        public Usercontroller(ILogger<Usercontroller> logger, studentDbContext studentDbContext,IConfiguration configuration)
        {
            _logger = logger;
            _studentDbContext = studentDbContext;
            _configuration = configuration;
        }

        [HttpGet]
        public ActionResult getUsers()
        {
            _logger.LogWarning("Entering Get Users");
            return Ok(_studentDbContext?.Users.ToList());
        }


        [HttpPost("add")]
        public async Task<ActionResult> addUser(User user)
        {

            _logger.LogWarning("Entering Add User");
            _studentDbContext.Users.Add(user);
            await _studentDbContext.SaveChangesAsync();
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult> login(User request)
        {

                var user = await _studentDbContext.Users.FindAsync(request.Username);

                if (user != null && user?.Password == request.Password)
                {

                    var token = createToken(user);
                    return Ok(token);

                }
                else
                {
                    return BadRequest("password not found");
                }



        }

        private string createToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.Username)
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("my favourite token is here thank you"));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims:claims,
                expires:DateTime.Now.AddDays(1),
                signingCredentials: cred

                );

            var Jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return Jwt;
        }

    }
}

