using Microsoft.EntityFrameworkCore;
using CryptographyTool;
using EntityModels;

namespace Services;

public class AutorizeService : IAutorizeService
{
    private readonly NewsAppDbContext _newsAppContext;
    private readonly int _pageSize = 20;

    public AutorizeService(NewsAppDbContext newsAppContext)
    {
        _newsAppContext = newsAppContext;
    }

    public async Task<IEnumerable<User>> RetrieveUsersAsync(int? offset, int? limit) => await _newsAppContext.Users
        .OrderByDescending(user => user.Registered)
        .Skip((offset ?? 0) * (limit ?? _pageSize))
        .Take(limit ?? _pageSize)
        .ToListAsync();

    public async Task<User?> RetrieveUserAsync(Guid id) => await _newsAppContext.Users.FindAsync(id);

    public async Task<User?> RegisterUserAsync(RegisterModel registerUser)
    {
        User user = (User)registerUser;
        User? duplicate = await _newsAppContext.Users.FirstOrDefaultAsync(dbuser => 
            dbuser.EmailAddress == user.EmailAddress);

        if (duplicate is not null)
        {
            return null;
        }

        (string salt,string hashed) = Protector.Encrypt(user.PasswordHash);
        user.PasswordSalt = salt;
        user.PasswordHash = hashed;

        await _newsAppContext.AddAsync(user);
        int affected = await _newsAppContext.SaveChangesAsync();
        if (affected == 1)
        {
            return await RetrieveUserAsync(user.UserId);
        }
        return null;
    }

    public async Task<User?> AutorizeUserAsync(AuthorizeModel authorize)
    {
        User? existed = await _newsAppContext.Users.FirstOrDefaultAsync(user => 
            user.EmailAddress == authorize.EmailAddress);

        if (existed is not null) 
        {
            if(Protector.CheckPassword(authorize.Password, existed.PasswordSalt, existed.PasswordHash))
            {
                existed.LastLogin = DateTime.Now;
                if (await _newsAppContext.SaveChangesAsync() == 1)
                {
                    return existed;
                }
            }
        }
        return null;
    }

    public async Task<User?> ChangePasswordAsync(AuthorizeModel model)
    {
        User? existed = await _newsAppContext.Users.FirstOrDefaultAsync(user =>
            user.EmailAddress == model.EmailAddress);

        if (existed is not null) 
        {
            (string salt, string hashed) = Protector.Encrypt(model.Password);
            existed.PasswordSalt = salt;
            existed.PasswordHash = hashed;

            int affected = await _newsAppContext.SaveChangesAsync();
            if (affected == 1) 
            {
                return existed;
            }
        }
        return null;
    }

    public async Task<User?> UpdateUserAsync(Guid id, User user)
    {
        User? existed = await _newsAppContext.Users.FindAsync(id);

        if (existed is not null) 
        {
            if (Protector.CheckPassword(user.PasswordHash, existed.PasswordSalt, existed.PasswordHash))
            {
                existed.UserName = user.UserName;
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
