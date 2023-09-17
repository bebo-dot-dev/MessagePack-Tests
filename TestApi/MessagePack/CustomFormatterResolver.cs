using MessagePack;
using MessagePack.Formatters;

namespace TestApi.MessagePack;

public class CustomFormatterResolver : IFormatterResolver
{
    private static CustomFormatterResolver _instance = null!;
    private EncryptionProvider _encryptionProvider = null!;
    
    public static CustomFormatterResolver GetInstance(EncryptionProvider encryptionProvider)
    {
        return _instance ??= new CustomFormatterResolver
        {
            _encryptionProvider = encryptionProvider
        };
    }
    
    public IMessagePackFormatter<T> GetFormatter<T>()
    {
        return (IMessagePackFormatter<T>)MessagePackObjFormatter.GetInstance(_instance._encryptionProvider);
    }
}