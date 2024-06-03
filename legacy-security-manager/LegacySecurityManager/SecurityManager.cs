using System;
using System.Runtime.InteropServices;

namespace LegacySecurityManager;

public class SecurityManager
{
    public void CreateSecurityUser()
    {
        PrintMessage("Enter a username");
        var username = ReadMessage();
        PrintMessage("Enter your full name");
        var fullName = ReadMessage();
        PrintMessage("Enter your password");
        var password = ReadMessage();
        PrintMessage("Re-enter your password");
        var confirmPassword = ReadMessage();

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

        PrintSavingDetails(username, fullName, array);
    }

    private void PrintSavingDetails(string username, string fullName, char[] array)
    {
        PrintMessage(String.Format("Saving Details for User ({0}, {1}, {2})\n", username, fullName, new string(array)));
    }

    protected virtual string ReadMessage()
    {
        return Console.ReadLine();
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