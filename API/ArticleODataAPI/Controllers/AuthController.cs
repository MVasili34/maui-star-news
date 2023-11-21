using Microsoft.AspNetCore.Mvc;
using Services;
using EntityModels;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ArticleODataAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAutorizeService _autorizeService;
    private readonly IConfiguration _configuration;

    public AuthController(
        IAutorizeService autorizeService,
        IConfiguration configuration)
    {
        _autorizeService = autorizeService;
        _configuration = configuration;
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
        return Created("api/Auth/", registered);
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
            authorized.PasswordHash = CreateToken(authorized);
            return Ok(authorized);
        }
        return Unauthorized();
    }

    private string CreateToken(User user)
    {
        List<Claim> claims = new() {
                new Claim(ClaimTypes.Name, user.EmailAddress),
                new Claim(ClaimTypes.Role, user.RoleId switch 
                { 
                    1 => "User",
                    2 => "Writer", 
                    3 => "Admin",
                    _ => "User"
                }),
        };

        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));

        SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha256Signature);

        JwtSecurityToken token = new(claims: claims,
                                     expires: DateTime.Now.AddHours(3),
                                     signingCredentials: creds
                                    );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
}
