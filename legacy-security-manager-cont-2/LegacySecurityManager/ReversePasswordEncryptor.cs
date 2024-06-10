using System;

namespace LegacySecurityManager;

public class ReversePasswordEncryptor : PasswordEncryptor {
    public string Encrypt(string password) {
        var array = password.ToCharArray();
        Array.Reverse((Array)array);
        var encryptedPassword = new string(array);
        return encryptedPassword;
    }
}