using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
namespace PG_Management_System.Helper;

public class AESEncryptionHelper
{
    

    private readonly string key = "3q2+7wRgTDoL8LmzBjHmOd5rHxmdozT7XbGgOGNlby8="; // Example Key
    private readonly string iv = "8N/TpA1YqflWs9rOR5sc3A=="; // Example IV
    public AESEncryptionHelper()
    {
        
    }
 
    public string Encrypt(string plainText)
    {
        using (Aes aes = Aes.Create())
        {
            // Decode the Base64 key and IV and ensure they are the correct length
            aes.Key = Convert.FromBase64String(key); // 32 bytes for AES-256
            aes.IV = Convert.FromBase64String(iv);   // 16 bytes for the IV

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                {
                    swEncrypt.Write(plainText);
                }

                var encrypted = msEncrypt.ToArray();
                return Convert.ToBase64String(encrypted);
            }
        }
    }

    public string Decrypt(string cipherText)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = Convert.FromBase64String(key); // 32 bytes for AES-256
            aes.IV = Convert.FromBase64String(iv);   // 16 bytes for the IV

            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
            using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
            {
                return srDecrypt.ReadToEnd();
            }
        }
    }
}