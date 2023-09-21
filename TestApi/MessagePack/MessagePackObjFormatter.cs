using System.Text;
using MessagePack;
using MessagePack.Formatters;

namespace TestApi.MessagePack;

public class MessagePackObjFormatter : IMessagePackFormatter<MessagePackObj>
{
    public static readonly MessagePackObjFormatter Instance;

    static MessagePackObjFormatter()
    {
        Instance = new MessagePackObjFormatter();
    }
    
    public void Serialize(
        ref MessagePackWriter writer,
        MessagePackObj messagePackObj,
        MessagePackSerializerOptions options)
    {
        writer.WriteArrayHeader(3);
        writer.WriteInt32(messagePackObj.Version);
        writer.WriteInt32(messagePackObj.Id);
        writer.WriteString(Encoding.UTF8.GetBytes(messagePackObj.EncryptedPassword));
    }

    public MessagePackObj Deserialize(
        ref MessagePackReader reader,
        MessagePackSerializerOptions options)
    {
        var version = 0;
        var id = 0;
        var encryptedPassword = "";
        
        int count = reader.ReadArrayHeader();
        for (int i = 0; i < count; i++)
        {
            switch (i)
            {
                case 0:
                    version = reader.ReadInt32();
                    break;
                case 1:
                    id = reader.ReadInt32();
                    break;
                case 2:
                    encryptedPassword = reader.ReadString()!;
                    break;
                default:
                    reader.Skip();
                    break;
            }
        }

        return new MessagePackObj
        {
            Version = version,
            Id = id,
            EncryptedPassword = encryptedPassword
        };
    }
}