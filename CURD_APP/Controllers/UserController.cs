using CURD_APP.Data;
using CURD_APP.Models;
using CURD_APP.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CURD_APP.Controllers
{
    [Route("api/v1")]
    public class UserController: ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly JwtService _jwtService;

        public UserController(ApplicationDbContext db, UserManager<IdentityUser> userManager, JwtService jwtService, SignInManager<IdentityUser> signInManager)
        {
            _db = db;
            _userManager = userManager;
            _jwtService = jwtService;
            _signInManager = signInManager;
        }

        // POST: api/Users/BearerToken
        [HttpPost("/login1")]
        public async Task<ActionResult<AuthenticationResponse>> CreateBearerToken([FromBody]Login request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Bad credentials");
            }

            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null)
            {
                return BadRequest("Bad credentials");
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!isPasswordValid)
            {
                return BadRequest("Bad credentials");
            }

            var token = _jwtService.CreateToken(user);

            return Ok(token);
        }

        [HttpPost("/register1")]
        public async Task<ActionResult<User>> PostUser([FromBody]User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userManager.CreateAsync(
                new IdentityUser() { UserName = user.UserName, Email = user.Email },
                user.Password
            );

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            user.Password = null;
            return CreatedAtAction("GetUser", new { username = user.UserName }, user);
        }
        [HttpPost("/logout")]
        //[Authorize]
        public async Task<ActionResult> Logout()
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            
            await _jwtService.InvalidateToken(token);
            await _signInManager.SignOutAsync();
            return Ok();
        }
        //[Authorize]
        [HttpGet("{username}")]
        public async Task<ActionResult<User>> GetUser(string username)
        {
            IdentityUser user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                return NotFound();
            }

            return new User
            {
                UserName = user.UserName,
                Email = user.Email
            };
        }

        [HttpPost("/register")]
        public ActionResult<User> Register([FromBody] User user)
        {
             if(!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            User item=_db.UserHandler.FirstOrDefault(u=>u.Email == user.Email);
            if (item != null)
            {
                return BadRequest("User already Exist");
            }

            //_db.Add(user);
            return Ok(user);

        }
        [HttpPost("/login")]
        public ActionResult<Login> Login([FromBody] Login user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Login item = _db.LoginHandler.FirstOrDefault(u => u.UserName == user.UserName);
            if (item == null)
            {
                return BadRequest("User Does not exist.Kindly Register First");
            }
            return Ok(user);

        }



    }
}
