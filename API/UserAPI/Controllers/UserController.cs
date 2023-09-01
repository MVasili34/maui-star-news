using Microsoft.AspNetCore.Mvc;
using Services;
using EntityModels;

namespace UserAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IAutorizeService _autorizeService;

    public UserController(IAutorizeService autorizeService)
    {
        _autorizeService = autorizeService;
    }

    //GET: api/User
    //QUERY: offset (INT)
    //QUERY: limit (INT)
    [HttpGet]
    public async Task<IEnumerable<User>> GetUsers([FromQuery] int? offset, [FromQuery] int? limit) => 
        await _autorizeService.RetrieveUsersAsync(offset, limit);

    //GET: api/User/[id]
    [HttpGet("{id:guid}", Name = nameof(GetUser))]
    [ProducesResponseType(200, Type = typeof(User))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetUser(Guid id)
    {
        User? user = await _autorizeService.RetrieveUserAsync(id);
        if(user is null)
        {
            return BadRequest();
        }
        return Ok(user);
    }

    //POST: api/User/register
    //BODY: User (JSON)
    [HttpPost("register")]
    [ProducesResponseType(201, Type = typeof(User))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> RegisterUser([FromBody] User user)
    {
        if (user is null)
        {
            return BadRequest();
        }

        User? registered = await _autorizeService.RegisterUserAsync(user);
        if (registered is null)
        {
            return BadRequest();
        }
        return CreatedAtRoute(routeName: nameof(GetUser),
                              routeValues: new { id = registered.UserId },
                              value: registered);
    }

    //PUT: api/User/login/[id]
    //BODY: AuthorizeModel (JSON)
    [HttpPut("login/{id:guid}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> LoginUser(Guid id, [FromBody] AuthorizeModel model)
    {
        bool? authorized = await _autorizeService.AutorizeUserAsync(id, model);
        if (authorized.HasValue && authorized.Value) 
        {
            return Ok();
        }
        return BadRequest();
    }

    //PUT: api/User/[id]
    //BODY: User (JSON)
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

    //DELETE: api/User/[id]
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
