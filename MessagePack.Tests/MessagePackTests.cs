using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TestApi.MessagePack;

namespace MessagePack.Tests;

public class MessagePackTests
{
    [Test]
    public void GivenTwoDictionaryItems_WhenSerializeAndDeserialize_AssertMessagePackSerializationException()
    {
        var dictionary = new Dictionary<int, MessagePackObj>
        {
            { 
                1, new MessagePackObj
                {
                    Id = 1,
                    EncryptedPassword = "cipherText"
                } 
            },
            { 
                2, new MessagePackObj
                {
                    Id = 2,
                    EncryptedPassword = "cipherText"
                } 
            }
        };
        
        var messagePackBytes = MessagePackSerializer.Serialize(
            dictionary,
            SerializerOptions.Standard);

        var ex = Assert.Throws<MessagePackSerializationException>(() => 
            MessagePackSerializer.Deserialize<Dictionary<int, MessagePackObj>>(
                messagePackBytes, SerializerOptions.Standard))!;
        
        ex.InnerException?.Message.Should().Contain("An item with the same key has already been added");
    }
}