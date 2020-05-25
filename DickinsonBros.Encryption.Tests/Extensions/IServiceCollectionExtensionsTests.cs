using DickinsonBros.Encryption.Abstractions;
using DickinsonBros.Encryption.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace DickinsonBros.Encryption.Tests.Extensions
{
    [TestClass]
    public class IServiceCollectionExtensionsTests
    {
        [TestMethod]
        public void AddPropelAPI_Should_Succeed()
        {
            // Arrange
            var serviceCollection = new ServiceCollection();
            // Act
            serviceCollection.AddEncryptionService();
            // Assert
            Assert.IsTrue(serviceCollection.Any(serviceDefinition => serviceDefinition.ServiceType == typeof(IEncryptionService) &&
                                                       serviceDefinition.ImplementationType == typeof(EncryptionService) &&
                                                       serviceDefinition.Lifetime == ServiceLifetime.Singleton));
        }
    }
}
