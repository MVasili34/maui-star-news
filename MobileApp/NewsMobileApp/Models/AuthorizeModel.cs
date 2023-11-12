namespace NewsMobileApp.Models;

public record AuthorizeModel
{
    public string EmailAddress { get; set; }
    public string Password { get; set; }
}