using System.Diagnostics.CodeAnalysis;

namespace DickinsonBros.Encryption.Models
{
    [ExcludeFromCodeCoverage]
    public class EncryptionServiceOptions
    {
        public string ThumbPrint { get; set; }
        public string StoreLocation { get; set; }
    }
}
