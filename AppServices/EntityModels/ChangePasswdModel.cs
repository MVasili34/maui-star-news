namespace EntityModels;

/// <summary>
/// Special record for changing password
/// </summary>
public record ChangePasswdModel
{
    public string EmailAddress { get; set; } = null!;
    public string OldPassword { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
}
