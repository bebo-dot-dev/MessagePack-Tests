using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TestApi.MessagePack;

namespace MessagePack.Tests;

public class MessagePackTests
{
    [Test]
    public void GivenTwoDictionaryItems_WhenSerializeAndDeserialize_AssertCanSerializeAndDeserialize()
    {
        var input = new Dictionary<int, MessagePackObj>
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
            input,
            SerializerOptions.MessagePackObjDictionary);

        var act = MessagePackSerializer.Deserialize<Dictionary<int, MessagePackObj>>(
            messagePackBytes,
            SerializerOptions.MessagePackObjDictionary);

        act.Should().BeEquivalentTo(input);
    }
}