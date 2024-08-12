using CSharpFunctionalExtensions;

namespace coIT.Libraries.ConfigurationManager.Cryptography
{
    public class NoCryptographyService : IDoCryptography
    {
        public Result<string> Decrypt(string encryptedText)
        {
            return encryptedText;
        }

        public Result<string> Encrypt(string plainText)
        {
            return plainText;
        }
    }
}
