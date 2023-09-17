using MessagePack;
using MessagePack.Formatters;

namespace TestApi.MessagePack;

public class StandardResolver : IFormatterResolver
{
    public static readonly StandardResolver Instance;
    
    static StandardResolver()
    {
        Instance = new StandardResolver();
    }
    
    public IMessagePackFormatter<T> GetFormatter<T>()
    {
        return (IMessagePackFormatter<T>)MessagePackObjFormatter.Instance;
    }
}