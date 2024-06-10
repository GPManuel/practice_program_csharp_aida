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

    [Test]
    public void move_character_to_left_when_shift_is_negative()
    {
        var password = "BCD";
        var caesarPasswordEncryptor = new CaesarPasswordEncryptor(-1);

        var encryptedPassword = caesarPasswordEncryptor.Encrypt(password);

        Assert.That(encryptedPassword, Is.EqualTo("ABC"));
    }

    [Test]
    public void not_encrypt_when_characters_not_belongs_to_alphabet()
    {
        var password = "A_B*C 1";
        var caesarPasswordEncryptor = new CaesarPasswordEncryptor(2);

        var encryptedPassword = caesarPasswordEncryptor.Encrypt(password);

        Assert.That(encryptedPassword, Is.EqualTo("C_D*E 1"));
    }
    
    [Test]
    public void wrap_around_when_you_reach_the_end_of_the_alphabet()
    {
        var password = "XYZ";
        var caesarPasswordEncryptor = new CaesarPasswordEncryptor(1);

        var encryptedPassword = caesarPasswordEncryptor.Encrypt(password);

        Assert.That(encryptedPassword, Is.EqualTo("YZA"));
    }

    [Test]
    public void wrap_around_when_you_reach_the_beginning_of_the_alphabet()
    {
        var password = "ABC";
        var caesarPasswordEncryptor = new CaesarPasswordEncryptor(-1);

        var encryptedPassword = caesarPasswordEncryptor.Encrypt(password);

        Assert.That(encryptedPassword, Is.EqualTo("ZAB"));
    }
}