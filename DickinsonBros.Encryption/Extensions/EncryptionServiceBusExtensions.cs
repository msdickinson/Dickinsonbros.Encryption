using DickinsonBros.Encryption.Abstractions;
using DickinsonBros.Encryption.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace DickinsonBros.Encryption.Extensions
{
    public static class EncryptionServiceExtensions
    {
        public static IServiceCollection AddEncryptionService(this IServiceCollection serviceCollection)
        {
            serviceCollection.TryAddSingleton<IEncryptionService, EncryptionService>();

            return serviceCollection;
        }
    }

}
