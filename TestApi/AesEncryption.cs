using System.Security.Cryptography;
using Microsoft.Extensions.Options;

namespace TestApi;

public class AesEncryption
{
    private readonly EncryptionOptions _options;
    
    public AesEncryption(IOptions<EncryptionOptions> options)
    {
        _options = options.Value;
    }

    public static (byte[] key, byte[] iv, int keySize) GenerateKey()
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
            rng.GetBytes(randomNumber);
        
        using var aes = Aes.Create();
        aes.Key = randomNumber;
        aes.GenerateIV();
        return (
            aes.Key, 
            aes.IV, 
            aes.KeySize);
    }
    
    public string Encrypt<T>(T clearValue)
    {
        using var aes = Aes.Create();
        aes.Key = _options.KeyBytes;
        aes.IV = _options.IVBytes;
        
        var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
        
        using var stream = new MemoryStream();
        using var csEncrypt = new CryptoStream(stream, encryptor, CryptoStreamMode.Write);
        using (var swEncrypt = new StreamWriter(csEncrypt))
            swEncrypt.Write(clearValue);
        
        return Convert.ToHexString(stream.ToArray()).ToLowerInvariant();
    }
    
    public string Decrypt(string cipherText)
    {
        var cipherBytes = Convert.FromHexString(cipherText);
        using var aes = Aes.Create();
        aes.Key = _options.KeyBytes;
        aes.IV = _options.IVBytes;

        var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
        
        using var stream = new MemoryStream(cipherBytes);
        using var csDecrypt = new CryptoStream(stream, decryptor, CryptoStreamMode.Read);
        using var srDecrypt = new StreamReader(csDecrypt);
        return srDecrypt.ReadToEnd();
    }
}