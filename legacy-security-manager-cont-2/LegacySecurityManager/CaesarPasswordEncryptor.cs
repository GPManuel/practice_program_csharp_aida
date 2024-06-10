using System.ComponentModel.Design;
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
        return new string(password.Select(character => Transform(character)).ToArray());
    }

    private char Transform(char character)
    {
        var index = alphabet.IndexOf(character);
        if (index == -1)
        {
            return character;
        }

        var characterPosition = _shift + index;
        if (characterPosition > alphabet.Length-1)
        {
            return alphabet[characterPosition-alphabet.Length];
        }
        else if (characterPosition < 0)
        {
            return alphabet[characterPosition+alphabet.Length];
        }
        return alphabet[characterPosition];
    }
}