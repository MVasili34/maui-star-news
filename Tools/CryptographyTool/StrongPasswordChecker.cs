namespace CryptographyTool;

public static class StrongPasswordChecker
{
    /// <summary>
    /// Метод проверки сложности пароля (не менее 1 прописной буквы, <br />
    ///  строчной буквы, цифры, специального символа и ни одного пробела)
    /// </summary>
    /// <param name="password">Строка (предполагаемый пароль)</param>
    /// <returns><see href="true"/>, если пароль сильный, иначе <see href="false"/></returns>
    public static bool PasswordCheck(string password)
    {
        if (password.Length < 8)
            return false;

        if(ContainsWhiteSpace(password))
            return false;

        if (!ContainsUppers(password))
            return false;

        if (!ContainsLowers(password))
            return false;

        if (!ContainsNumber(password))
            return false;

        if (!ContainsSpecial(password))
            return false;

        return true;
    }

    private static bool ContainsWhiteSpace(string password) => password.Any(let => let == ' ');

    private static bool ContainsUppers(string password) => password.Any(char.IsUpper);

    private static bool ContainsLowers(string password) => password.Any(char.IsLower);

    private static bool ContainsNumber(string password) => password.Any(char.IsNumber);

    private static bool ContainsSpecial(string password)
    {
        bool contains = false;

        foreach (char c in password) 
        {
            if (!char.IsLetterOrDigit(c))
            {
                contains = true;
                break;
            }
        }
        return contains;
    }
}
