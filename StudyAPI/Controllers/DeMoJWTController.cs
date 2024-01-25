using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudyAPI.Models;
using StudyAPI.Models.data;

namespace StudyAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DeMoJWTController : ControllerBase
    {
        private readonly AppDbContext de;

        public DeMoJWTController(AppDbContext context)
        {
            de = context;
        }

        [HttpGet("[action]")]
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
            
            return Ok(new { username, pass });  
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
    }
}
