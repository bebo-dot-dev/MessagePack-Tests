using System.Runtime.Serialization;

namespace TestApi;

[DataContract]
public sealed record MessagePackObj
{
    private string? _decryptedPassword;
    
    [DataMember(Order = 0)] 
    public int Version { get; init; } = 1;

    [DataMember(Order = 1)] 
    public string EncryptedPassword { get; init; } = null!;

    public string GetPassword(AesEncryption aesEncryption)
    {
        if (!string.IsNullOrEmpty(_decryptedPassword)) return _decryptedPassword;
        _decryptedPassword = aesEncryption.Decrypt(EncryptedPassword);
        return _decryptedPassword;
    }
}