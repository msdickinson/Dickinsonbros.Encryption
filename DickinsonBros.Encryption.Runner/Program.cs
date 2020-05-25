using DickinsonBros.Encryption.Abstractions;
using DickinsonBros.Encryption.Extensions;
using DickinsonBros.Encryption.Models;
using DickinsonBros.Encryption.Runner.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
            await new Program().DoMain();
        }
        async Task DoMain()
        {
            try
            {
                using (var applicationLifetime = new ApplicationLifetime())
                {
                    var services = InitializeDependencyInjection();
                    ConfigureServices(services, applicationLifetime);

                    using (var provider = services.BuildServiceProvider())
                    {
                        var encryptionService = (EncryptionService)provider.GetRequiredService<IEncryptionService>();


                        var stringToEncrypt = "Data Source=.;Initial Catalog=DickinsonBros.Telemetry.Runner.Database;Integrated Security=True;";
                        Console.WriteLine("String To Encrpyt" + Environment.NewLine + stringToEncrypt + Environment.NewLine);

                        var encyptedString = encryptionService.Encrypt(stringToEncrypt);
                        Console.WriteLine("Encrypted String" + Environment.NewLine + encyptedString + Environment.NewLine);

                        var decryptedString = encryptionService.Decrypt(encyptedString);
                        Console.WriteLine("Decrypted String" + Environment.NewLine + decryptedString + Environment.NewLine);
                    }
                    applicationLifetime.StopApplication();
                    await Task.CompletedTask.ConfigureAwait(false);
                }
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
        }

        private void ConfigureServices(IServiceCollection services, ApplicationLifetime applicationLifetime)
        {
            services.AddOptions();
            services.AddLogging(config =>
            {
                config.AddConfiguration(_configuration.GetSection("Logging"));

                if (Environment.GetEnvironmentVariable("BUILD_CONFIGURATION") == "DEBUG")
                {
                    config.AddConsole();
                }
            });
            services.AddSingleton<IApplicationLifetime>(applicationLifetime);
            services.AddEncryptionService();
            services.Configure<EncryptionServiceOptions>(_configuration.GetSection(nameof(EncryptionServiceOptions)));

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

