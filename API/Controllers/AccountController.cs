
using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController: BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        public AccountController(DataContext context, ITokenService tokenService){ //context inside contructor is dependency injection which is used for database interaction
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("register")]  // POST: api/Account/register
        public async Task<ActionResult<UserDto>> Register(/*[FromBody]*/RegisterDto registerDto){ //we added [FromBody] (we'll remove it for now and let our baseapicontroller do the job)attribute because we removed hte
        // [APIController] attribute from the base API controller so now we need to tell our code where to look for the object and for the validations
        // we'll need to do that here itself.

            if (await UserExists(registerDto.username)) return BadRequest("Username is taken");


            using var hmac = new HMACSHA512(); // Why did we use the 'using' keyword before initialization? - so when we create an instance of a class like of HMACSHA512() in 
            // this case, this consumes some memory and if we want to dispose this automatically after its work is done, we can use 'using' keyword. And why we can use this
            // 'using' keyword is because the class that HMACSHA512() implements, implements a class called IDisposable and whatever class implements this can use 'using'.

            var user = new AppUser{
                UserName = registerDto.username.ToLower(), 
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.password)), // we accessed 'GetBytes' method because our PasswordHash is a byte array.
                PasswordSalt = hmac.Key
            };

            _context.Users.Add(user); // this doesnt add the entity to the db but only tracks our entity in memory.
            await _context.SaveChangesAsync();// now this will add the user in our db

            return new UserDto {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            }; 
        
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto logindto){
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == logindto.Username);
            if(user == null) return Unauthorized("Invalid Username!");
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(logindto.Password));
            for(int i = 0; i < computedHash.Length; i++){
                if(computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password!");
            }
            
            return new UserDto {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            }; 

        }

         private async Task<bool> UserExists(string username) {

            return await _context.Users.AnyAsync(x => x.UserName == username.ToLower());
         }


    }
}