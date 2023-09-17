using MessagePack;
using MessagePack.Formatters;

namespace TestApi.MessagePack;

public class EncryptionEnabledFormatter : IMessagePackFormatter<Dictionary<int, MessagePackObj>>
{
    private static EncryptionEnabledFormatter _instance = null!;
    private EncryptionProvider _encryptionProvider = null!;
    
    public static EncryptionEnabledFormatter GetInstance(EncryptionProvider encryptionProvider)
    {
        return _instance ??= new EncryptionEnabledFormatter
        {
            _encryptionProvider = encryptionProvider
        };
    }
    
    public void Serialize(
        ref MessagePackWriter writer, 
        Dictionary<int, MessagePackObj> dictionary, 
        MessagePackSerializerOptions options)
    {
        foreach (var value in dictionary.Values)
        {
            var raw = MessagePackSerializer.Serialize(value);
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
            obj.Password = _encryptionProvider.Decrypt(obj.EncryptedPassword);
            dict.Add(obj.Id, obj);
        }
        
        return dict;
        
        /*
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
        */
    }
}