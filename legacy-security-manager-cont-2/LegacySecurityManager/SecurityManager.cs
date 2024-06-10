using LegacySecurityManager.infrastructure;

namespace LegacySecurityManager;

public class SecurityManager
{
    private readonly Notifier _notifier;
    private ConsoleUserDataRequester _userDataRequester;
    private PasswordEncrypter _reversePasswordEncrypter;

    public SecurityManager(Notifier notifier, Input input, PasswordEncrypter passwordEncrypter) {
        _notifier = notifier;
        _userDataRequester = new ConsoleUserDataRequester(input);
        _reversePasswordEncrypter = passwordEncrypter;
    }

    public static void CreateUser() {
        Notifier notifier = new ConsoleNotifier();
        new SecurityManager(notifier, new ConsoleInput(), new ReversePasswordEncrypter()).CreateValidUser();
    }

    public void CreateValidUser()
    {
        var userData = _userDataRequester.Request();

        if (userData.PasswordsDoNotMatch())
        {
            NotifyPasswordDoNotMatch();
            return;
        }

        if (userData.IsPasswordToShort())
        {
            NotifyPasswordIsToShort();
            return;
        }

        var encryptedPassword = _reversePasswordEncrypter.Encrypt(userData.Password());
        NotifyUserCreation(userData.UserName(), userData.FullName(), encryptedPassword);
    }

    private void NotifyPasswordIsToShort()
    {
        Print("Password must be at least 8 characters in length");
    }

    private void NotifyPasswordDoNotMatch()
    {
        Print("The passwords don't match");
    }

    private void NotifyUserCreation(string username, string fullName, string encryptedPassword)
    {
        Print($"Saving Details for User ({username}, {fullName}, {encryptedPassword})\n");
    }

    private void Print(string message)
    {
        _notifier.Notify(message);
    }
}