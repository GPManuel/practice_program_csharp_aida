using NUnit.Framework;

namespace LegacySecurityManager.Tests
{
    /*
     *ABC -->(0) ABC
       ABC -->(1) BCD
       ABC -->(-1) ZAB
     */
    public class CaesarPasswordEncryptorTest
    {
        [Test]
        public void password_not_change_when_shift_is_0()
        {
            var password = "ABC";
            var caesarPasswordEncrypter = new CaesarPasswordEncryptor(0);

            var encryptedPassword = caesarPasswordEncrypter.Encrypt(password);

            Assert.That(encryptedPassword, Is.EqualTo(password));
        }
    }
}
