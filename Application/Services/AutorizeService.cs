using Microsoft.EntityFrameworkCore;
using CryptographyTool;

namespace Services;

public class AutorizeService : IAutorizeService
{
    private readonly NewsAppDbContext _newsAppContext;
    private readonly int _pageSize = 20;

    public AutorizeService(NewsAppDbContext newsAppContext)
    {
        _newsAppContext = newsAppContext;
    }

    /// <summary>
    /// Метод постраничного получения пользователей из базы данных
    /// </summary>
    /// <param name="offset">Номер страницы (начиная с 0)</param>
    /// <param name="limit">Число пользователей на странице</param>
    /// <returns><see langword="limit" /> пользователей с <see langword="offset" /> страницы</returns>
    public async Task<IEnumerable<User>> RetrieveUsersAsync(int? offset, int? limit) => await _newsAppContext.Users
        .Skip((offset ?? 0) * (limit ?? _pageSize))
        .Take(limit ?? _pageSize)
        .ToListAsync();

    /// <summary>
    /// Метод получения пользователя по идентификатору из базы данных
    /// </summary>
    /// <param name="id">Идентификатор пользователя</param>
    /// <returns>Объект <see cref="User" />, иначе <see langword="null" /></returns>
    public async Task<User?> RetrieveUserAsync(Guid id) => await _newsAppContext.Users.FindAsync(id);

    /// <summary>
    /// Метод регистрации пользователя по предоставленным данным
    /// </summary>
    /// <param name="user">Объект <see cref="User"/> с вложенным паролем</param>
    /// <returns>Объект <see cref="User"/>, если регистрация успешна, иначе <see langword="null"/></returns>
    public async Task<User?> RegisterUserAsync(User user)
    {
        User? dublicate = await _newsAppContext.Users.FirstOrDefaultAsync(dbuser => dbuser.EmailAddress == user.EmailAddress);
        if (dublicate is not null)
        {
            return null;
        }
        var registerData = Protector.Encrypt(user.PasswordHash);
        user.PasswordSalt = registerData.salt;
        user.PasswordHash = registerData.hashed;
        await _newsAppContext.AddAsync(user);
        int affected = await _newsAppContext.SaveChangesAsync();
        if (affected == 1)
        {
            return await RetrieveUserAsync(user.UserId);
        }
        return null;
    }

    /// <summary>
    /// Метод авторизации пользователя
    /// </summary>
    /// <param name="id">Действительный идентификатор пользователя</param>
    /// <param name="user">Объект <see cref="User"/> с вложенным паролем</param>
    /// <returns>Если данные верны <see langword="true"/>, иначе <see langword="false"/>.
    /// Если же пользователя не существуюет вовсе, то <see langword="null"/></returns>
    public async Task<bool?> AutorizeUserAsync(Guid id, AuthorizeModel authorize)
    {
        User? existed = await _newsAppContext.Users.FindAsync(id);
        if (existed is not null) 
        {
            if(existed.EmailAddress == authorize.EmailAddress &&
                Protector.CheckPassword(authorize.Password, existed.PasswordSalt, existed.PasswordHash))
            {
                return true;
            }
            return false;
        }
        return null;
    }

    /// <summary>
    /// Метод обновления данных пользователя
    /// </summary>
    /// <param name="id">Идентификатор пользователя</param>
    /// <param name="user">Объект <see cref="User"/></param>
    /// <returns><see cref="User"/>, если данные обновлены, иначе <see langword="null"/></returns>
    public async Task<User?> UpdateUserAsync(Guid id, User user)
    {
        User? existed = await _newsAppContext.Users.FindAsync(id);
        if (existed is not null) 
        {
            if (!Protector.CheckPassword(user.PasswordHash, existed.PasswordSalt, existed.PasswordHash))
            {
                existed.UserName = user.UserName;
                existed.EmailAddress = user.EmailAddress;
                existed.DateOfBirth = user.DateOfBirth;
                existed.Phone = user.Phone;

                int affected = await _newsAppContext.SaveChangesAsync();
                if (affected == 1)
                {
                    return existed;
                }
            }
        }
        return null;
    }

    /// <summary>
    /// Метод удаления пользователя по идентификатору 
    /// </summary>
    /// <param name="id">Идентификатор пользователя</param>
    /// <returns>Если пользователь удалён, то <see langword="true"/>, иначе <see langword="false"/>.
    /// Если же пользователя не существуюет вовсе, то <see langword="null"/></returns>
    public async Task<bool?> DeleteUserAsync(Guid id)
    {
        User? existed = await _newsAppContext.Users.FindAsync(id);
        if (existed is not null) 
        {
            _newsAppContext.Users.Remove(existed);
            int affected = await _newsAppContext.SaveChangesAsync();
            if (affected == 1) 
            {
                return true;
            }
            return false;
        }
        return null;
    }
}
