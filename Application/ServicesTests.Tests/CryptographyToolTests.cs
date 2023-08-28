using static CryptographyTool.Protector;

namespace ServicesTests.Tests;

public class CryptographyToolTests
{
    [Fact]
    public void CheckPasswordTest()
    {
        string password = "pa$$w0rd";
        var registration = Encrypt(password);

        bool check = CheckPassword(password, registration.salt, registration.hashed);

        Assert.True(check);
    }

    [Fact]
    public void DifferentEntityTest()
    {
        string password = "pa$$w0rd";
        var registration1 = Encrypt(password);
        var registration2 = Encrypt(password);

        //При одном и том же пароле, но разной соли проверка должна выдать false
        bool check = CheckPassword(password, registration2.salt, registration1.hashed);

        Assert.False(check);
    }
}
