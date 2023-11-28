using Bogus;

namespace Services;

public class DataGenerator
{
    /// <summary>
    /// Generates <see cref="User"/> instance with random data.
    /// </summary>
    /// <returns>
    /// A <see cref="User"/> object with random data.
    /// </returns>
    public static User GenerateUser() => new Faker<User>("ru")
        .RuleFor(user => user.UserName, user => user.Name.FirstName())
        .RuleFor(user => user.EmailAddress, user => user.Internet.Email())
        .RuleFor(user => user.DateOfBirth, user => user.Date.BetweenDateOnly(
        DateOnly.FromDateTime(DateTime.Now.AddYears(-80).Date),
        DateOnly.FromDateTime(DateTime.Now.AddYears(-18).Date)))
        .RuleFor(user => user.PasswordSalt, user => user.Hashids.Encode(Random.Shared.Next(100, 1000)))
        .RuleFor(user => user.PasswordHash, user => user.Hashids.Encode(Random.Shared.Next(100, 1000)))
        .RuleFor(user => user.RoleId, user => user.Random.Int(1, 2))
        .Generate();

    /// <summary>
    /// Generates <see cref="RegisterModel"/> instance with random data.
    /// </summary>
    /// <returns>
    /// A <see cref="RegisterModel"/> object with random data.
    /// </returns>
    public static RegisterModel GenerateRegisterMode() => new Faker<RegisterModel>("ru")
        .RuleFor(user => user.UserName, user => user.Name.FirstName())
        .RuleFor(user => user.EmailAddress, user => user.Internet.Email())
        .Generate();

    /// <summary>
    /// Generates <see cref="Article"/> instance with random data.
    /// </summary>
    /// <returns>
    /// A <see cref="Article"/> object with random data.
    /// </returns>
    public static Article GenerateArticle() => new Faker<Article>("ru")
        .RuleFor(art => art.Title, art => art.Name.FullName())
        .RuleFor(art => art.Subtitle, art => art.Lorem.Word())
        .RuleFor(art => art.SectionId, art => art.Random.Int(1, 5))
        .RuleFor(art => art.Image, art => art.Image.LoremPixelUrl())
        .RuleFor(art => art.Text, art => art.Lorem.Text())
        .Generate();

    /// <summary>
    /// Generates <see cref="Section"/> instance with random data.
    /// </summary>
    /// <returns>
    /// A <see cref="Section"/> object with random data.
    /// </returns>
    public static Section GenerateSection() => new Faker<Section>("ru")
        .RuleFor(sect => sect.Name, sect => sect.Lorem.Word())
        .RuleFor(sect => sect.Description, sect => sect.Lorem.Word())
        .Generate();

}
