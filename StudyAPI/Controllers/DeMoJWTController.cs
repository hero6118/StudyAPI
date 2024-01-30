using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StudyAPI.Models;
using StudyAPI.Models.data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudyAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DeMoJWTController : ControllerBase
    {
        private readonly AppDbContext de;

        private IConfiguration _config;
        public DeMoJWTController(AppDbContext context, IConfiguration config)
        {
            de = context;
            _config = config;
        }


        [HttpPost("[action]")]
        public IActionResult Login ([FromForm]string username, [FromForm]string pass)
        {

            var hasher = new PasswordHasher<string>();
            if (!de.Users.Any(p => p.Name == username))
                return Ok(new{ check = false, ms = "Username does not exist" });

            var user1 = de.Users.Single(p => p.Name == username);

            var check = hasher.VerifyHashedPassword(username, user1?.Pass, pass);
            if(check == 0)
                return Ok(new { check = false, ms = "Invalid password" });
            IActionResult resp = Unauthorized();

            var tokenstr = GenerateJSONWebToken(user1);

           

            
            return Ok(new { check =true, ms="login success!", jwt = tokenstr});  
        }
        private string GenerateJSONWebToken(User model)
        {
            var abcc = _config["Jwt:key"];
            var bytelength = Encoding.UTF8.GetBytes(abcc);
            if (bytelength.Length < 256)
                return "Fail";

            var securitykey = new SymmetricSecurityKey(bytelength);
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,model.Name),
                new Claim (JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim ("UserName", model.Name),
                new Claim ("IdUser", model.Id.ToString() ?? "")

            };
            var expires = DateTime.UtcNow.AddSeconds(50);
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims: claims,
                expires: expires,
               
                signingCredentials: credentials
            );
            token.Payload["exp"] = ((DateTimeOffset)expires).ToUnixTimeSeconds();
            var encodetoken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodetoken;



   
        }
        [HttpPost("[action]")]
        public IActionResult Register([FromForm] string username, [FromForm] string pass, [FromForm] string confirmpass)
        {
            if(pass != confirmpass)
                return Ok(new { check = false,ms ="Incorect confirm pass!" }); 
            if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(confirmpass) || string.IsNullOrEmpty(pass))
                return Ok(new { check = false, ms = "Field can be not blank" });


            var hasher = new PasswordHasher<string>();

            var user = new User()
            {
                Id = Guid.NewGuid(),
                Name = username,
            };
            user.Pass = hasher.HashPassword(user.Name, pass);
            de.Add(user);
            de.SaveChanges();
            return Ok(new { check = true, ms = "Register Success!" });

        }
        //;ấy du lieu từ payload
        [Authorize(Roles ="Admin")]
        [HttpPost("[action]")]
        public string Post()
        {
            var identity =  HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claims  =  identity.Claims.ToList();
            var username = claims[0].Value;
            return "hello: " + username;
        }
        [Authorize]
        
        [HttpGet ("GetValue")]
        public ActionResult<IEnumerable<string>> GetValue()
        {
            var isAuthenticated = User.Identity.IsAuthenticated;
            if(!isAuthenticated)
                return new string[] { "het han", "dl 2" };

            return new string[] { "du lieu da duoc lay", "dl 2" };
        }
    }
}
