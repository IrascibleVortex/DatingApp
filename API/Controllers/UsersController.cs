using API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

//We'll need to get a request from our angular client to hit this controller.
[ApiController]
[Route("api/[controller]")]  //  /api/users
public class UsersController : ControllerBase
{
    private readonly DataContext _context;

    public UsersController(DataContext context)
    {
        _context = context;
    }

    // this end point needs to be hit in order to get the users.
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers() {

        var users = await _context.Users.ToListAsync();
        return users;
    }

    [HttpGet("{id}")]  //api/users/2
    public async Task<ActionResult<AppUser>> GetUser(int id){
        return await _context.Users.FindAsync(id);
    }
}
