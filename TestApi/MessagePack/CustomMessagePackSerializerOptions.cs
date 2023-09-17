using MessagePack;

namespace TestApi.MessagePack;

public static class CustomMessagePackSerializerOptions
{
    public static MessagePackSerializerOptions Custom(EncryptionProvider encryptionProvider) => OptionsHelper.CustomOptions(encryptionProvider);
    
    private static class OptionsHelper
    {
        public static MessagePackSerializerOptions CustomOptions(EncryptionProvider encryptionProvider)
        {
            var resolver = CustomFormatterResolver.GetInstance(encryptionProvider);
            return new MessagePackSerializerOptions(resolver);
        } 
    }
}