using MessagePack;
using MessagePack.Formatters;

namespace TestApi.MessagePack;

public class MessagePackObjDictionaryFormatter : IMessagePackFormatter<Dictionary<int, MessagePackObj>>
{
    public static readonly MessagePackObjDictionaryFormatter Instance;

    static MessagePackObjDictionaryFormatter()
    {
        Instance = new MessagePackObjDictionaryFormatter();
    }
    
    public void Serialize(
        ref MessagePackWriter writer,
        Dictionary<int, MessagePackObj> dictionary,
        MessagePackSerializerOptions options)
    {
        var valueFormatter = options.Resolver.GetFormatterWithVerify<MessagePackObj>();
        writer.WriteArrayHeader(dictionary.Count);
        foreach (var entry in dictionary)
        {
            valueFormatter.Serialize(ref writer, entry.Value, options);
        }
    }

    public Dictionary<int, MessagePackObj> Deserialize(
        ref MessagePackReader reader,
        MessagePackSerializerOptions options)
    {
        var valueFormatter = options.Resolver.GetFormatterWithVerify<MessagePackObj>();
        var dict = new Dictionary<int, MessagePackObj>();
        int count = reader.ReadArrayHeader();
        for (int i = 0; i < count; i++)
        {
            var obj = valueFormatter.Deserialize(ref reader, options);
            dict.Add(obj.Id, obj);
        }

        return dict;
    }
}