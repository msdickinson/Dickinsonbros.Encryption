using DickinsonBros.Encryption.Abstractions;
using DickinsonBros.Encryption.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DickinsonBros.Encryption.Runner
{
    class Program
    {
        IConfiguration _configuration;
        async static Task Main(string[] args)
        {
            await new Program().DoMain(args);
        }
        async Task DoMain(string[] args)
        {
            var services = InitializeDependencyInjection();
            ConfigureServices(services);
            using (var provider = services.BuildServiceProvider())
            {
                try
                {
                    var encryptionService = provider.GetRequiredService<IEncryptionService>();

                    var stringToEncrypt = "String123!";
                    Console.WriteLine("String To Encrpyt" + Environment.NewLine + stringToEncrypt + Environment.NewLine);

                    var encyptedString = encryptionService.Encrypt(stringToEncrypt);
                    Console.WriteLine("Encrypted String" + Environment.NewLine + encyptedString + Environment.NewLine);

                    var decryptedString = encryptionService.Decrypt(encyptedString);
                    Console.WriteLine("Decrypted String" + Environment.NewLine + decryptedString + Environment.NewLine);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                finally
                {
                    Console.WriteLine("End...");
                    Console.ReadKey();
                }
                await Task.CompletedTask;
            }
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<EncryptionSettings>(_configuration.GetSection("EncryptionSettings"));
            services.AddSingleton<IEncryptionService, EncryptionService>();
        }

        IServiceCollection InitializeDependencyInjection()
        {
            var aspnetCoreEnvironment = Environment.GetEnvironmentVariable("BUILD_CONFIGURATION");
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false)
                .AddJsonFile($"appsettings.{aspnetCoreEnvironment}.json", true);
            _configuration = builder.Build();
            var services = new ServiceCollection();
            services.AddSingleton(_configuration);
            return services;
        }
    }
}

