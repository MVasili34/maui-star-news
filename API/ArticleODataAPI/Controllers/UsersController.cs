using Microsoft.AspNetCore.Mvc;
using Services;
using EntityModels;
using Microsoft.AspNetCore.Authorization;

namespace ArticleODataAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IAuthUsersService _usersService;

    public UsersController(IAuthUsersService usersService) => _usersService = usersService;

    [HttpGet]
    [Authorize(Roles="Admin")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetUsers([FromQuery] int? offset, [FromQuery] int? limit)
    {
        try
        {
            return Ok(await _usersService.RetrieveUsersAsync(offset, limit));
        }
        catch (Exception ex) 
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id:guid}", Name = nameof(GetUser))]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(200, Type = typeof(User))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetUser(Guid id)
    {
        User? user = await _usersService.RetrieveUserAsync(id);
        if(user is null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpPost("adminsetpswd")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> UpdatePasswordByAdmin([FromBody] AuthorizeModel model)
    {
        if (model is null)
        {
            return BadRequest();
        }
        if (await _usersService.ChangePasswordAdminAsync(model) is null)
        {
            return NotFound();
        }
        return Ok();
    }

    [HttpPut("updadm/{id:guid}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> UpdateUserByAdmin(Guid id, [FromBody] User user)
    {
        if (user is null || user.UserId != id)
        {
            return BadRequest();
        }

        User? existed = await _usersService.RetrieveUserAsync(id);
        if (existed is null)
        {
            return NotFound();
        }

        if (await _usersService.UpdateUserAdminAsync(id, user) is null)
        {
            return BadRequest();
        }
        return NoContent();
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin,User,Writer")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> UpdateUser(Guid id, [FromBody] User user)
    {
        if (user is null || user.UserId != id)
        {
            return BadRequest();
        }

        User? existed = await _usersService.RetrieveUserAsync(id);
        if (existed is null) 
        {
            return NotFound();
        }

        if(await _usersService.UpdateUserAsync(id, user) is null)
        {
            return BadRequest();
        }
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        bool? status = await _usersService.DeleteUserAsync(id);

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
