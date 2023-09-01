using Services;

namespace ServicesTests.Tests;

public class AuthorizeSeviceTests
{
    private readonly IAutorizeService _authorizeService;

    public AuthorizeSeviceTests(IAutorizeService authorizeService)
    {
        _authorizeService = authorizeService;
    }

    [Fact]
    public async Task RegisterAndLoginSuccessTest()
    {
        User generated = DataGenerator.GenerateUser();
        generated.PasswordHash = "pa$$w0rd";
        await _authorizeService.RegisterUserAsync(generated);

        AuthorizeModel authorizeModel = new()
        {
            UserId = generated.UserId,
            EmailAddress = generated.EmailAddress,
            Password = "pa$$w0rd"
        };
        bool? authorized = await _authorizeService.AutorizeUserAsync(generated.UserId, authorizeModel);

        Assert.True(authorized);
    }

    [Fact]
    public async Task RegisterAndLoginFailureTest()
    {
        User generated = DataGenerator.GenerateUser();
        generated.PasswordHash = "pa$$w0rd";
        await _authorizeService.RegisterUserAsync(generated);

        AuthorizeModel authorizeModel = new()
        {
            UserId = generated.UserId,
            EmailAddress = generated.EmailAddress,
            Password = "passwird"
        };
        bool? authorized = await _authorizeService.AutorizeUserAsync(generated.UserId, authorizeModel);

        Assert.False(authorized);
    }

    [Fact]
    public async Task ChangeAccountInfoTest()
    {
        User generated = DataGenerator.GenerateUser();
        generated.PasswordHash = "pa$$w0rd";
        await _authorizeService.RegisterUserAsync(generated);

        User changedCopy = new()
        {
            UserId = generated.UserId,
            UserName = generated.UserName,
            EmailAddress = generated.EmailAddress,
            DateOfBirth = generated.DateOfBirth,
            Phone = "+1 (245) 678 92",
            PasswordHash = "pa$$w0rd"
        };
        User? updated = await _authorizeService.UpdateUserAsync(generated.UserId, changedCopy);

        Assert.Equal(generated, updated);
    }

    [Fact]
    public async Task DeleteUserTest()
    {
        User generated = DataGenerator.GenerateUser();
        generated.PasswordHash = "pa$$w0rd";
        await _authorizeService.RegisterUserAsync(generated);

        bool? condition = await _authorizeService.DeleteUserAsync(generated.UserId);

        Assert.True(condition);
    }
}
