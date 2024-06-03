using NUnit.Framework;
using System.Collections.Generic;

namespace LegacySecurityManager.Tests;

public class SecurityManagerTest
{
    [Test]
    public void confirm_password_and_password_are_not_the_same()
    {
        Queue<string> queue  = new Queue<string>();
        queue.Enqueue("Carlos");
        queue.Enqueue("Carlos Inguanzo");
        queue.Enqueue("Carlos123_Ing");
        queue.Enqueue("Carlos");

        var securityManeger = new SecurityManagerForTesting(queue);
        securityManeger.CreateSecurityUser();

        var expectedMessage = new List<string> { "Enter a username", "Enter your full name", "Enter your password", "Re-enter your password", "The passwords don't match" };
        Assert.That(expectedMessage , Is.EquivalentTo(securityManeger._messages));
    }

    public class SecurityManagerForTesting: SecurityManager
    {
        private Queue<string> _queue;
        public List<string> _messages;

        public SecurityManagerForTesting(Queue<string> queue)
        {
            _queue = queue;
            _messages = new List<string>();
        }

        protected override string ReadMessage()
        {
            return _queue.Dequeue();
        }

        protected override void PrintMessage(string message)
        {
            _messages.Add(message);
        }
    }
}
