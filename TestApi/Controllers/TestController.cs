using MessagePack;
using Microsoft.AspNetCore.Mvc;
using TestApi.MessagePack;

namespace TestApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class TestController : ControllerBase
{
    private readonly EncryptionProvider _encryptionProvider;

    public TestController(EncryptionProvider encryptionProvider)
    {
        _encryptionProvider = encryptionProvider;
    }

    [HttpGet]
    public Task<IActionResult> GenerateEncryptionKey()
    {
        var (key, iv, keySize) = EncryptionProvider.GenerateKey();
        var response = new
        {
            Key = Convert.ToHexString(key).ToLowerInvariant(),
            IV = Convert.ToHexString(iv).ToLowerInvariant(),
            KeySize = keySize
        };
        return Task.FromResult<IActionResult>(Ok(response));
    }

    [HttpPost]
    public Task<IActionResult> EncryptValue([FromBody] EncryptionModel request)
    {
        var cipherText = _encryptionProvider.Encrypt(request.Value);
        return Task.FromResult<IActionResult>(Ok(cipherText));
    }
    
    [HttpPost]
    public Task<IActionResult> DecryptValue([FromBody] EncryptionModel request)
    {
        var clearText = _encryptionProvider.Decrypt(request.Value);
        return Task.FromResult<IActionResult>(Ok(clearText));
    }

    [HttpPost]
    public Task<IActionResult> CreateMessagePack([FromBody] EncryptionModel request)
    {
        var cipherText = _encryptionProvider.Encrypt(request.Value);
        var newMsgPack = new MessagePackObj
        {
            Id = 1,
            EncryptedPassword = cipherText
        };
        var messagePackBytes = MessagePackSerializer.Serialize(newMsgPack, MessagePackSerializerOptions.Standard);
        System.IO.File.WriteAllBytes("Resources/default.msgpack", messagePackBytes);
        return Task.FromResult<IActionResult>(Ok(cipherText));
    }

    [HttpGet]
    public Task<IActionResult> GetMessagePack()
    {
        var messagePackBytes = System.IO.File.ReadAllBytes("Resources/default.msgpack");
        var msgPack = MessagePackSerializer.Deserialize<MessagePackObj>(messagePackBytes, MessagePackSerializerOptions.Standard);
        var decipheredPassword = msgPack.GetPassword(_encryptionProvider);
        var response = new
        {
            cipherPassword = msgPack.EncryptedPassword,
            password = decipheredPassword
        };
        return Task.FromResult<IActionResult>(Ok(response));
    }
    
    [HttpPost]
    public Task<IActionResult> CreateMessagePackCustom([FromBody] EncryptionModel request)
    {
        var cipherText = _encryptionProvider.Encrypt(request.Value);
        
        var dictionary = new Dictionary<int, MessagePackObj>
        {
            { 
                1, new MessagePackObj
                {
                    Id = 1,
                    EncryptedPassword = cipherText
                } 
            },
            { 
                2, new MessagePackObj
                {
                    Id = 2,
                    EncryptedPassword = cipherText
                } 
            }
        };
        
        var messagePackBytes = MessagePackSerializer.Serialize(
            dictionary,
            SerializerOptions.MessagePackObjDictionaryWithEncryption(_encryptionProvider));
        
        System.IO.File.WriteAllBytes("Resources/custom.msgpack", messagePackBytes);
        return Task.FromResult<IActionResult>(Ok(cipherText));
    }
    
    [HttpGet]
    public Task<IActionResult> GetMessagePackCustom()
    {
        var messagePackBytes = System.IO.File.ReadAllBytes("Resources/custom.msgpack");
        var dictionary = MessagePackSerializer.Deserialize<Dictionary<int, MessagePackObj>>(messagePackBytes, SerializerOptions.MessagePackObjDictionaryWithEncryption(_encryptionProvider));
        return Task.FromResult<IActionResult>(Ok(dictionary));
    }
}