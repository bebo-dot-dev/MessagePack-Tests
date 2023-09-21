using MessagePack;
using MessagePack.Formatters;

namespace TestApi.MessagePack;

public class MessagePackObjResolver : IFormatterResolver
{
    public static readonly MessagePackObjResolver Instance;
    
    static MessagePackObjResolver()
    {
        Instance = new MessagePackObjResolver();
    }
    
    public IMessagePackFormatter<T> GetFormatter<T>()
    {
        if (typeof(T) == typeof(Dictionary<int, MessagePackObj>))
            return (IMessagePackFormatter<T>)MessagePackObjDictionaryFormatter.Instance;
        
        if (typeof(T) == typeof(MessagePackObj))
            return (IMessagePackFormatter<T>)MessagePackObjFormatter.Instance;

        throw new ArgumentOutOfRangeException();
    }
}