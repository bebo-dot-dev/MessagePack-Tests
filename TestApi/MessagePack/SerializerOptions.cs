using MessagePack;

namespace TestApi.MessagePack;

public static class SerializerOptions
{
    public static MessagePackSerializerOptions MessagePackObjDictionaryWithEncryption(EncryptionProvider encryptionProvider) => 
        OptionsHelper.WithEncryption(encryptionProvider);
    
    public static MessagePackSerializerOptions MessagePackObjDictionary => OptionsHelper.WithNoEncryption();
    
    private static class OptionsHelper
    {
        public static MessagePackSerializerOptions WithEncryption(EncryptionProvider encryptionProvider)
        {
            var resolver = EncryptionEnabledResolver.GetInstance(encryptionProvider);
            return new MessagePackSerializerOptions(resolver);
        }
        
        public static MessagePackSerializerOptions WithNoEncryption()
        {
            return new MessagePackSerializerOptions(MessagePackObjResolver.Instance);
        } 
    }
}