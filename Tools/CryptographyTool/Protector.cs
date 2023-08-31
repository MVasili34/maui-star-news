using System.Text;
using System.Security.Cryptography;
using static System.Convert;

namespace CryptographyTool;

public class Protector
{
    /// <summary>
    /// Метод шифрования данных
    /// </summary>
    /// <param name="password">Входная строка</param>
    /// <returns>Кортеж (соль + хешированная и подсоленная строка)</returns>
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
    /// Метод проверки пароля
    /// </summary>
    /// <param name="password">Входной нехешированный пароль</param>
    /// <param name="salt">Соль</param>
    /// <param name="hashedPassword">Подсоленный и хешированный пароль</param>
    /// <returns><see langword="true" />, если подсоленные хеши паролей равны, иначе <see langword="false" /></returns>
    public static bool CheckPassword(string password, string salt, string hashedPassword)
    {

        string saltedhashedPassword = SaltAndHashPassword(password, salt);

        return (saltedhashedPassword == hashedPassword);
    }

    /// <summary>
    /// Метод подсоления и хеширования строки
    /// </summary>
    /// <param name="password">Входная трока</param>
    /// <param name="saltText">Соль</param>
    /// <returns>Подсоленная и хешированная строка</returns>
    private static string SaltAndHashPassword(string password, string saltText)
    {
        using (SHA256 arm = SHA256.Create())
        {
            string saltedPassword = password + saltText;
            return ToBase64String(arm.ComputeHash(
                Encoding.Unicode.GetBytes(saltedPassword)));
        }
    }
}