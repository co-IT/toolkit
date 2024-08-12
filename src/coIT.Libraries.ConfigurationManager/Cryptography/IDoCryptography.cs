using CSharpFunctionalExtensions;

namespace coIT.Libraries.ConfigurationManager.Cryptography
{
    public interface IDoCryptography
    {
        public Result<string> Encrypt(string plainText);
        public Result<string> Decrypt(string encryptedText);
    }
}
