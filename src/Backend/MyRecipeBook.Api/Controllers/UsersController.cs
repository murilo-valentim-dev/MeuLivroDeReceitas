using Microsoft.AspNetCore.Mvc;
using MyRecipeBook.Communication.Requests;

namespace MyRecipeBook.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    [HttpPost()]
    public IActionResult Register([FromBody] RequestRegisterUserAccountJson request)
    {
        // Registro a conta de uma pessoa no sistema


        return Created();
    }
}
