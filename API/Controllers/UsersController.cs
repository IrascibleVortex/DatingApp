using API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;



//We'll need to get a request from our angular client to hit this controller.
//[ApiController]
//[Route("api/[controller]")]  //  /api/users
[Authorize] // putting it here will make sure all the endpoints in this class are only accessed by authorised users
public class UsersController : BaseApiController
{
    private readonly DataContext _context;

    public UsersController(DataContext context)
    {
        _context = context;
    }

    [AllowAnonymous] // this will override the [authorize] attribute ,i.e, this overrides all authentications but the vice versa is not true
    // this end point needs to be hit in order to get the users.
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers() {

        var users = await _context.Users.ToListAsync();
        return users;
    }

    //[Authorize] // this will make sure only the users authorized are able to call this api request
    [HttpGet("{id}")]  //api/users/2
    public async Task<ActionResult<AppUser>> GetUser(int id){
        return await _context.Users.FindAsync(id);
    }
}
