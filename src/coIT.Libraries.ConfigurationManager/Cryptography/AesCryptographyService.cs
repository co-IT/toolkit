using System.Security.Cryptography;
using System.Text.Json;
using CSharpFunctionalExtensions;
using static System.Net.Mime.MediaTypeNames;

namespace coIT.Libraries.ConfigurationManager.Cryptography
{
    public class AesCryptographyService : IDoCryptography
    {
        private readonly Aes aes;

        public static Result<AesCryptographyService> FromKey(string serializedKeyIvPairBase64)
        {
            var serializedKeyIvPair = Base64Decode(serializedKeyIvPairBase64);
            var keyIvPair = JsonSerializer.Deserialize<Tuple<byte[], byte[]>>(serializedKeyIvPair);
            if (keyIvPair is null)
                return Result.Failure<AesCryptographyService>(
                    "Schlüssel kann nicht geladen werden"
                );

            var service = new AesCryptographyService(keyIvPair.Item1, keyIvPair.Item2);
            return Result.Success(service);
        }

        public AesCryptographyService()
        {
            aes = Aes.Create();
        }

        private AesCryptographyService(byte[] key, byte[] iv)
        {
            aes = Aes.Create();
            aes.Key = key;
            aes.IV = iv;
        }

        public string SchlüsselExportieren()
        {
            var serializedKey = JsonSerializer.Serialize(
                new Tuple<byte[], byte[]>(aes.Key, aes.IV)
            );
            return Base64Encode(serializedKey);
        }

        // https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.aes?view=net-8.0
        public Result<string> Encrypt(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                return Result.Failure<string>("Text ist leer und kann nicht verschlüsselt werden");

            byte[] encrypted;

            // Create an encryptor to perform the stream transform.
            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            // Create the streams used for encryption.
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (
                    CryptoStream csEncrypt = new CryptoStream(
                        msEncrypt,
                        encryptor,
                        CryptoStreamMode.Write
                    )
                )
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        //Write all data to the stream.
                        swEncrypt.Write(plainText);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }

            return Convert.ToBase64String(encrypted);
        }

        public Result<string> Decrypt(string encryptedText)
        {
            if (string.IsNullOrEmpty(encryptedText))
                return Result.Failure<string>(
                    "Konfiguration ist leer und kann nicht entschlüssselt werden"
                );

            byte[] cipherText;
            try
            {
                cipherText = Convert.FromBase64String(encryptedText);
            }
            catch (Exception ex)
            {
                return Result.Failure<string>(
                    "Konfiguration ist invalide und kann nicht entschlüsselt werden"
                );
            }

            if (cipherText == null || cipherText.Length <= 0)
                return Result.Failure<string>(
                    "Konfiguration ist leer und kann nicht entschlüssselt werden"
                );

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an decryptor to perform the stream transform.
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            // Create the streams used for encryption.
            using (MemoryStream msDecrypt = new MemoryStream(cipherText))
            {
                using (
                    CryptoStream csDecrypt = new CryptoStream(
                        msDecrypt,
                        decryptor,
                        CryptoStreamMode.Read
                    )
                )
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        // Read the decrypted bytes from the decrypting stream
                        // and place them in a string.
                        plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }

            return plaintext;
        }

        private static string Base64Encode(string text)
        {
            var textBytes = System.Text.Encoding.UTF8.GetBytes(text);
            return Convert.ToBase64String(textBytes);
        }

        private static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
