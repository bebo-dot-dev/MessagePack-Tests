using System.Runtime.Serialization;

namespace TestApi.MessagePack;

[DataContract]
public sealed record MessagePackObj
{
    private string? _password;
    
    [DataMember(Order = 0)] 
    public int Version { get; init; } = 1;
    
    [DataMember(Order = 1)] 
    public int Id { get; init; }

    [DataMember(Order = 2)] 
    public string EncryptedPassword { get; init; } = null!;
    
    [IgnoreDataMember]
    public string Password { get; set; } = null!;

    public string GetPassword(EncryptionProvider provider)
    {
        if (_password is not null) return _password;
        _password = provider.Decrypt(EncryptedPassword);
        return _password;
    }
}