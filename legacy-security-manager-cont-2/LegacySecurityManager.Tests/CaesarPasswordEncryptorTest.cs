using NUnit.Framework;

namespace LegacySecurityManager.Tests;

public class CaesarPasswordEncryptorTest
{
    [Test]
    public void password_not_change_when_shift_is_0()
    {
        var password = "ABC";
        var caesarPasswordEncryptor = new CaesarPasswordEncryptor(0);

        var encryptedPassword = caesarPasswordEncryptor.Encrypt(password);

        Assert.That(encryptedPassword, Is.EqualTo(password));
    }

    [Test]
    public void move_character_to_right_when_shift_is_positive()
    {
        var password = "ABC";
        var caesarPasswordEncryptor = new CaesarPasswordEncryptor(2);

        var encryptedPassword = caesarPasswordEncryptor.Encrypt(password);

        Assert.That(encryptedPassword, Is.EqualTo("CDE"));
    }
}