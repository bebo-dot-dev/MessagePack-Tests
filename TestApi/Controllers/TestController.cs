using MessagePack;
using Microsoft.AspNetCore.Mvc;

namespace TestApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class TestController : ControllerBase
{
    private readonly AesEncryption _aesEncryption;

    public TestController(AesEncryption aesEncryption)
    {
        _aesEncryption = aesEncryption;
    }

    [HttpGet]
    public Task<IActionResult> GenerateEncryptionKey()
    {
        var (key, iv, keySize) = AesEncryption.GenerateKey();
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
        var cipherText = _aesEncryption.Encrypt(request.Value);
        return Task.FromResult<IActionResult>(Ok(cipherText));
    }
    
    [HttpPost]
    public Task<IActionResult> DecryptValue([FromBody] EncryptionModel request)
    {
        var clearText = _aesEncryption.Decrypt(request.Value);
        return Task.FromResult<IActionResult>(Ok(clearText));
    }

    [HttpPost]
    public Task<IActionResult> CreateMessagePack([FromBody] EncryptionModel request)
    {
        var cipherText = _aesEncryption.Encrypt(request.Value);
        var newMsgPack = new MessagePackObj
        {
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
        var msgPack = MessagePackSerializer.Deserialize<MessagePackObj>(messagePackBytes);
        var decipheredPassword = msgPack.GetPassword(_aesEncryption);
        var response = new
        {
            cipherPassword = msgPack.EncryptedPassword,
            password = decipheredPassword
        };
        return Task.FromResult<IActionResult>(Ok(response));
    }
}