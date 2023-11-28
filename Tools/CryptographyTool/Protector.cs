using System.Text;
using System.Security.Cryptography;
using static System.Convert;

namespace CryptographyTool;

/// <summary>
/// The <see cref="Protector"/> class provides methods for password encryption and verification.
/// </summary>
public class Protector
{
    /// <summary>
    /// Encrypts password by hashing him with <see cref="SHA256"/>
    /// </summary>
    /// <param name="password">The password to encrypt</param>
    /// <returns>A tuple containing the salt and the hashed password.</returns>
    public static (string salt, string hashed) Encrypt(string password)
    {
        RandomNumberGenerator random = RandomNumberGenerator.Create();
        byte[] salt = new byte[16];
        random.GetBytes(salt);
        string saltText = ToBase64String(salt);

        string saltedHashedPassword = SaltAndHashPassword(password, saltText);

        return (saltText, saltedHashedPassword);
    }

    /// <summary>
    /// Checks if a password matches the hashed password.
    /// </summary>
    /// <param name="password">The password to check</param>
    /// <param name="salt">The salt used in the original encryption</param>
    /// <param name="hashedPassword">The original hashed and salted password</param>
    /// <returns><see langword="true" /> if the password matches the hashed password, <see langword="false" /> otherwise.</returns>
    public static bool CheckPassword(string password, string salt, string hashedPassword)
    {

        string saltedhashedPassword = SaltAndHashPassword(password, salt);

        return (saltedhashedPassword == hashedPassword);
    }

    /// <summary>
    /// Salts and hashes a password.
    /// </summary>
    /// <param name="password">The password to salt and hash</param>
    /// <param name="saltText">The salt to use in the hashing process</param>
    /// <returns>The salted and hashed password.</returns>
    private static string SaltAndHashPassword(string password, string saltText)
    {
        string saltedPassword = password + saltText;
        return ToBase64String(SHA256.HashData(Encoding.Unicode.GetBytes(saltedPassword)));
    }
}