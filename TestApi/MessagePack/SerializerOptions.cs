using MessagePack;

namespace TestApi.MessagePack;

public static class SerializerOptions
{
    public static MessagePackSerializerOptions EncryptionEnabled(EncryptionProvider encryptionProvider) => 
        OptionsHelper.Encryption(encryptionProvider);
    
    public static MessagePackSerializerOptions Standard => OptionsHelper.NoEncryption();
    
    private static class OptionsHelper
    {
        public static MessagePackSerializerOptions Encryption(EncryptionProvider encryptionProvider)
        {
            var resolver = EncryptionEnabledResolver.GetInstance(encryptionProvider);
            return new MessagePackSerializerOptions(resolver);
        }
        
        public static MessagePackSerializerOptions NoEncryption()
        {
            return new MessagePackSerializerOptions(StandardResolver.Instance);
        } 
    }
}