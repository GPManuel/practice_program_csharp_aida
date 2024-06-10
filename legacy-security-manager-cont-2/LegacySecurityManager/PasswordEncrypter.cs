namespace LegacySecurityManager;

public interface PasswordEncrypter {
    string Encrypt(string password);
}