using Microsoft.AspNetCore.Mvc;
using Services;
using EntityModels;

namespace ArticleODataAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IAutorizeService _autorizeService;

    public UsersController(IAutorizeService autorizeService) => 
        _autorizeService = autorizeService;

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetUsers([FromQuery] int? offset, [FromQuery] int? limit)
    {
        try
        {
            return Ok(await _autorizeService.RetrieveUsersAsync(offset, limit));
        }
        catch (Exception ex) 
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id:guid}", Name = nameof(GetUser))]
    [ProducesResponseType(200, Type = typeof(User))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetUser(Guid id)
    {
        User? user = await _autorizeService.RetrieveUserAsync(id);
        if(user is null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpPost("register")]
    [ProducesResponseType(201, Type = typeof(RegisterModel))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterModel user)
    {
        if (user is null)
        {
            return BadRequest();
        }

        User? registered = await _autorizeService.RegisterUserAsync(user);
        if (registered is null)
        {
            return BadRequest("Пользователь с таким электронным адресом уже существует!");
        }
        return CreatedAtRoute(routeName: nameof(GetUser),
                              routeValues: new { id = registered.UserId },
                              value: registered);
    }

    [HttpPost("login")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    public async Task<IActionResult> LoginUser([FromBody] AuthorizeModel model)
    {
        if (model is null)
        {
            return BadRequest();
        }
        User? authorized = await _autorizeService.AutorizeUserAsync(model);
        if (authorized is not null) 
        {
            return Ok(authorized);
        }
        return Unauthorized();
    }

    [HttpPut("changepswd")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> UpdatePassword([FromBody] AuthorizeModel model)
    {
        if(model is null)
        {
            return BadRequest();
        }
        if(await _autorizeService.ChangePasswordAsync(model) is null)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpPut("updadm/{id:guid}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> UpdateUserByAdmin(Guid id, [FromBody] User user)
    {
        if (user is null || user.UserId != id)
        {
            return BadRequest();
        }

        User? existed = await _autorizeService.RetrieveUserAsync(id);
        if (existed is null)
        {
            return NotFound();
        }

        if (await _autorizeService.UpdateUserAdminAsync(id, user) is null)
        {
            return BadRequest();
        }
        return NoContent();
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> UpdateUser(Guid id, [FromBody] User user)
    {
        if (user is null || user.UserId != id)
        {
            return BadRequest();
        }

        User? existed = await _autorizeService.RetrieveUserAsync(id);
        if (existed is null) 
        {
            return NotFound();
        }

        if(await _autorizeService.UpdateUserAsync(id, user) is null)
        {
            return BadRequest();
        }
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        bool? status = await _autorizeService.DeleteUserAsync(id);

        if (!status.HasValue)
        {
            return NotFound();
        }

        if (status.Value)
        {
            return NoContent();
        }

        return BadRequest();
    }
}
