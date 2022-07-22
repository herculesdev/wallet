using System.Security.Cryptography;
using System.Text;

namespace Wallet.Shared.Helpers;

public static class Crypto
{
    public static string Encrypt(string data)
    {
        if (string.IsNullOrEmpty(data))
            return data;
        
        var iv = new byte[16];  
        byte[] array;  
  
        using (Aes aes = Aes.Create())  
        {  
            aes.Key = Encoding.UTF8.GetBytes(Utils.EncryptionKey);  
            aes.IV = iv;  
  
            var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);  
  
            using (var memoryStream = new MemoryStream())  
            {  
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))  
                {  
                    using (var streamWriter = new StreamWriter(cryptoStream))  
                    {  
                        streamWriter.Write(data);  
                    }  
  
                    array = memoryStream.ToArray();  
                }  
            }  
        }  
  
        return Convert.ToBase64String(array);
    }

    public static string Decrypt(string data)
    {
        if (string.IsNullOrEmpty(data))
            return data;
        
        var iv = new byte[16];  
        var buffer = Convert.FromBase64String(data);

        using Aes aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(Utils.EncryptionKey);
        aes.IV = iv;
        var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

        using var memoryStream = new MemoryStream(buffer);
        using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
        using var streamReader = new StreamReader(cryptoStream);
        return streamReader.ReadToEnd();
    }
}
