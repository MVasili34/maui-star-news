namespace Services;

public interface IAutorizeService
{
    Task<IEnumerable<User>> RetrieveUsersAsync(int? offset, int? limit);
    Task<User?> RetrieveUserAsync(Guid id);
    Task<bool?> AutorizeUserAsync(Guid id, AuthorizeModel user);
    Task<User?> RegisterUserAsync(User user);
    Task<User?> UpdateUserAsync(Guid id, User user);
    Task<bool?> DeleteUserAsync(Guid id);
}
