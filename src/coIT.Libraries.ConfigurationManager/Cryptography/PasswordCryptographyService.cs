using System.Security.Cryptography;
using CSharpFunctionalExtensions;

namespace coIT.Libraries.ConfigurationManager.Cryptography
{
    public class PasswordCryptographyService : IDoCryptography
    {
        private readonly AesCryptographyService _crypgraphyService;

        public PasswordCryptographyService(string password)
        {
            var keyAndIv = PasswordToKeyAndIV(password);
            _crypgraphyService = new AesCryptographyService(keyAndIv.Key, keyAndIv.IV);
        }

        private static (byte[] Key, byte[] IV) PasswordToKeyAndIV(string password)
        {
            // Da die Passwörter in Bitwarden liegen und bereits extrem lang sind
            // und dieser Code nie großflächig für Invididuelle Passwörter eingesetzt
            // werden sollte, kann man entgegen Best Practices hier ohne Salt arbeiten
            byte[] salt = [00];
            var schlüsselableiter = new Rfc2898DeriveBytes(
                password,
                salt,
                1000,
                HashAlgorithmName.SHA512
            );

            var key = schlüsselableiter.GetBytes(32);
            var iv = schlüsselableiter.GetBytes(16);

            return (key, iv);
        }

        public Result<string> Decrypt(string encryptedText)
        {
            return _crypgraphyService.Decrypt(encryptedText);
        }

        public Result<string> Encrypt(string plainText)
        {
            return _crypgraphyService.Encrypt(plainText);
        }
    }
}
