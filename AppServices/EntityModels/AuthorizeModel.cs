namespace EntityModels;

/// <summary>
/// Special record for authorize and changing password
/// </summary>
public record AuthorizeModel
{
    public string EmailAddress { get; set; } = null!;
    public string Password { get; set; } = null!;
}
