using System.Text;
using MessagePack;
using MessagePack.Formatters;

namespace TestApi.MessagePack;

public class MessagePackObjFormatter : IMessagePackFormatter<MessagePackObj>
{
    private static MessagePackObjFormatter _instance = null!;
    private EncryptionProvider _encryptionProvider = null!;
    
    public static MessagePackObjFormatter GetInstance(EncryptionProvider encryptionProvider)
    {
        return _instance ??= new MessagePackObjFormatter
        {
            _encryptionProvider = encryptionProvider
        };
    }
    
    public void Serialize(
        ref MessagePackWriter writer, 
        MessagePackObj value, 
        MessagePackSerializerOptions options)
    {
        if (value == null)
        {
            writer.WriteNil();
            return;
        }

        writer.WriteArrayHeader(2);
        writer.WriteInt32(value.Version);
        writer.WriteString(Encoding.UTF8.GetBytes(value.EncryptedPassword));
    }

    public MessagePackObj Deserialize(
        ref MessagePackReader reader, 
        MessagePackSerializerOptions options)
    {
        if (reader.TryReadNil()) return null;
        
        options.Security.DepthStep(ref reader);

        var version = 0;
        var encryptedPassword = string.Empty;
        var password = string.Empty;
        
        var fieldCount = reader.ReadArrayHeader();
        for (var i = 0; i < fieldCount; i++)
        {
            switch (i)
            {
                case 0:
                    version = reader.ReadInt32();
                    break;
                case 1:
                    encryptedPassword = reader.ReadString()!;
                    password = _encryptionProvider.Decrypt(encryptedPassword);
                    break;
                default:
                    reader.Skip();
                    break;
            }
        }

        reader.Depth--;
        
        return new MessagePackObj
        {
            Version = version,
            EncryptedPassword = encryptedPassword,
            Password = password
        };
    }
}