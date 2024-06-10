using System.Linq;

namespace LegacySecurityManager;

public class CaesarPasswordEncryptor : PasswordEncryptor
{
    private readonly int _shift;
    private const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    public CaesarPasswordEncryptor(int shift)
    {
        _shift = shift;
    }

    public string Encrypt(string password)
    {
        if (_shift > 0)
        {
            return new string(password.Select(character => Transform(character)).ToArray());
        }
        return password;
    }

    private char Transform(char character)
    {
        var passwordTranformed = 'a';
        var index = alphabet.IndexOf(character);
        passwordTranformed = alphabet[_shift + index];

        return passwordTranformed;
    }
}