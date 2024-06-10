namespace LegacySecurityManager;

public class CaesarPasswordEncryptor : PasswordEncryptor
{
    private readonly int _shift;

    public CaesarPasswordEncryptor(int shift)
    {
        _shift = shift;
    }

    public string Encrypt(string password)
    {
        return password;
    }
}