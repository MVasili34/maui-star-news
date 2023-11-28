namespace CryptographyTool;

public static class StrongPasswordChecker
{
    /// <summary>
    /// Check if the password is strong (at least 1 upper letter, <br />
    /// lower letter, number, special symbol and no spaces)
    /// </summary>
    /// <param name="password">Password to check</param>
    /// <returns><see href="true"/> if password is strong, <see href="false"/> otherwise</returns>
    public static bool PasswordCheck(string password)
    {
        if (string.IsNullOrEmpty(password))
            return false;

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
