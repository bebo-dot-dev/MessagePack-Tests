using MessagePack;
using MessagePack.Formatters;

namespace TestApi.MessagePack;

public class EncryptionEnabledResolver : IFormatterResolver
{
    private static EncryptionEnabledResolver _instance = null!;
    private EncryptionProvider _encryptionProvider = null!;
    
    public static EncryptionEnabledResolver GetInstance(EncryptionProvider encryptionProvider)
    {
        return _instance ??= new EncryptionEnabledResolver
        {
            _encryptionProvider = encryptionProvider
        };
    }
    
    public IMessagePackFormatter<T> GetFormatter<T>()
    {
        return (IMessagePackFormatter<T>)EncryptionEnabledFormatter.GetInstance(_instance._encryptionProvider);
    }
}