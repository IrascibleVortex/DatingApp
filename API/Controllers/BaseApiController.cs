
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    //the below [APIController] attribute has some powers: 
    // 1. automatically bind to our endpoint methods.
    // 2. this will check for validation even before we reach out API controller, so on entering blank username it'll throw an error
    // as we have added validation , so wont even reach the AccountController.
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController: ControllerBase
    {
        
    }
}