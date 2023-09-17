using MessagePack;
using MessagePack.Formatters;

namespace TestApi.MessagePack;

public class MessagePackObjFormatter : IMessagePackFormatter<Dictionary<int, MessagePackObj>>
{
    public static readonly MessagePackObjFormatter Instance;

    static MessagePackObjFormatter()
    {
        Instance = new MessagePackObjFormatter();
    }
    
    public void Serialize(
        ref MessagePackWriter writer, 
        Dictionary<int, MessagePackObj> dictionary, 
        MessagePackSerializerOptions options)
    {
        foreach (var raw in dictionary.Values.Select(value => MessagePackSerializer.Serialize(value)))
        {
            writer.WriteRaw(raw);
        }
    }

    public Dictionary<int, MessagePackObj> Deserialize(
        ref MessagePackReader reader, 
        MessagePackSerializerOptions options)
    {
        var dict = new Dictionary<int, MessagePackObj>();
        while (!reader.End)
        {
            var raw = reader.ReadRaw();
            var obj = MessagePackSerializer.Deserialize<MessagePackObj>(raw);
            dict.Add(obj.Id, obj);
        }
        
        return dict;
    }
}