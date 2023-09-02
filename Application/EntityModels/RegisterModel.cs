namespace EntityModels;

/// <summary>
/// Специальная запись для регистрации пользователей, независящая
/// от сущностныъ моделей <para />
/// Определён метод приведения записи к типу <see cref="User"/>
/// </summary>
public record RegisterModel
{
    public string UserName { get; set; } = null!;
    public string EmailAddress { get; set; } = null!;
    public string Password { get; set; } = null!;
    public int RoleId { get; set; } = 1;

    /// <summary>
    /// Метод приведения записи к объекту типа <see cref="User"/>
    /// </summary>
    /// <param name="model">Объект <see cref="User"/></param>
    public static explicit operator User(RegisterModel model) => new User()
    {
        UserName = model.UserName,
        EmailAddress = model.EmailAddress,
        PasswordSalt = string.Empty,
        PasswordHash = model.Password,
        RoleId = model.RoleId
    };
}
