using System;

namespace LegacySecurityManager;

public class SecurityManager
{
    public void CreateSecurityUser()
    {
        PrintMessage("Enter a username");
        var username = Console.ReadLine();
        PrintMessage("Enter your full name");
        var fullName = Console.ReadLine();
        PrintMessage("Enter your password");
        var password = Console.ReadLine();
        PrintMessage("Re-enter your password");
        var confirmPassword = Console.ReadLine();

        if (password != confirmPassword)
        {
            PrintMessage("The passwords don't match");
            return;
        }

        if (password.Length < 8)
        {
            PrintMessage("Password must be at least 8 characters in length");
            return;
        }

        // Encrypt the password (just reverse it, should be secure)
        char[] array = password.ToCharArray();
        Array.Reverse(array);

        Console.WriteLine("Saving Details for User ({0}, {1}, {2})\n", username, fullName, new string(array));
    }

    protected virtual void PrintMessage(string message)
    {
        Console.WriteLine(message);
    }

    public static void CreateUser()
    {
        new SecurityManager().CreateSecurityUser();
    }
}