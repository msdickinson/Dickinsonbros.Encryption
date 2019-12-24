using System.Diagnostics.CodeAnalysis;

namespace DickinsonBros.Encryption
{
    [ExcludeFromCodeCoverage]
    public class EncryptionSettings
    {
        public string ThumbPrint { get; set; }
        public string StoreLocation { get; set; }
    }
}
