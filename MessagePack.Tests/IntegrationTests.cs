using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using TestApi;

namespace MessagePack.Tests;

public class IntegrationTests
{
    private WebApplicationFactory<Program> _application = null!;
    
    [SetUp]
    public void SetUp()
    {
        _application = new WebApplicationFactory<Program>().WithWebHostBuilder(_ =>
        {            
        });
        
    }

    [TearDown]
    public async Task TearDown()
    {
        await _application.DisposeAsync();
    }
    
    [Test]
    public async Task EncryptDecryptTest()
    {
        var client = _application.CreateClient();
        client.BaseAddress = new Uri("http://localhost:5129");

        const string inputClearText = "This is some test clear text";

        var encryptPayload = new EncryptionModel { Value = inputClearText };
        var encryptContent = new StringContent(JsonSerializer.Serialize(encryptPayload), Encoding.UTF8, "application/json");
        var encryptResponse = await client.PostAsync("/Test/EncryptValue", encryptContent);
        var encryptedText = await encryptResponse.Content.ReadAsStringAsync();
        
        var decryptPayload = new EncryptionModel { Value = encryptedText };
        var decryptContent = new StringContent(JsonSerializer.Serialize(decryptPayload), Encoding.UTF8, "application/json");
        var decryptResponse = await client.PostAsync("/Test/DecryptValue", decryptContent);
        var decipheredText = await decryptResponse.Content.ReadAsStringAsync();
        
        decipheredText.Should().Be(inputClearText);
    }
}