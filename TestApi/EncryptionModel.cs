using System.ComponentModel.DataAnnotations;

namespace TestApi;

public sealed class EncryptionModel
{
    [Required]
    public string Value { get; init; } = null!;
}