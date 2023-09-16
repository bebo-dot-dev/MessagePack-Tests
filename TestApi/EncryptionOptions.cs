using System.ComponentModel.DataAnnotations;

namespace TestApi;

public sealed class EncryptionOptions
{
    public const string Section = "Encryption";

    [Required]
    public string Key { get; init; } = null!;
    
    [Required]
    public string IV { get; init; } = null!;

    public byte[] KeyBytes => Convert.FromHexString(Key);
    
    public byte[] IVBytes => Convert.FromHexString(IV);
}