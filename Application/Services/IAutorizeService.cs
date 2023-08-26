namespace Services;

public interface IAutorizeService
{
    Task<User?> RetrieveUserAsync(Guid id);
    Task<bool?> AutorizeUserAsync(string email, string password);
    Task<IEnumerable<User>> RetrieveUsersAsync();
    Task<User?> RegisterUserAsync(User user);
    Task<User?> UpdateUserAsync(Guid id, User user);
    Task<bool?> DeleteUserAsync(Guid id);
}
