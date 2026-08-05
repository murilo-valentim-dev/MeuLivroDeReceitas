using Microsoft.AspNetCore.Mvc;
using MyRecipeBook.Communication.Requests;

namespace MyRecipeBook.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    public IActionResult Register(RequestRegisterUserAccountJson request)
    {
        // Implement user registration logic here
        return Created();
    }
}
