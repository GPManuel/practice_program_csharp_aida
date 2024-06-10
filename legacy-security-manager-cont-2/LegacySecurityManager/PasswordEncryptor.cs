namespace LegacySecurityManager;

public interface PasswordEncryptor {
    string Encrypt(string password);
}