using System.Security.Cryptography;
using System.Text;
using Wallet.Domain.Helpers;

namespace Wallet.Domain.ValueObjects;

public class Password
{
    public string EncryptedValue { get; private set; } = string.Empty;
    public string Value { get => Decrypt(EncryptedValue); init => EncryptedValue = Encrypt(value); }
    
    public Password() { }

    public Password(string value)
    {
        Value = Encrypt(value);
    }

    private string Encrypt(string data)
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

    private string Decrypt(string data)
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

    public override string ToString() => EncryptedValue;

    public static implicit operator Password(string value) => new Password(value);
}
