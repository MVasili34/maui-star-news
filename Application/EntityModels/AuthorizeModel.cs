namespace EntityModels;

/// <summary>
/// Специальная запись для авторизации пользователей, 
/// независимая от сущностных моделей
/// </summary>
public record AuthorizeModel
{
    public Guid UserId { get; set; }
    public string EmailAddress { get; set; } = null!;
    public string Password { get; set; } = null!;
}
