namespace EntityModels;

/// <summary>
/// Record to register users
/// </summary>
public record RegisterModel
{
    public string UserName { get; set; } = null!;
    public string EmailAddress { get; set; } = null!;
    public string Password { get; set; } = null!;
    public int RoleId { get; set; } = 1;

    /// <summary>
    /// Castring record to <see cref="User"/> instance
    /// </summary>
    /// <param name="model"><see cref="RegisterModel"/> instance</param>
    public static explicit operator User(RegisterModel model) => new User
    {
        UserName = model.UserName,
        EmailAddress = model.EmailAddress,
        PasswordSalt = string.Empty,
        PasswordHash = model.Password,
        RoleId = model.RoleId
    };
}
