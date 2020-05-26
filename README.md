# Dickinsonbros.Encryption
<a href="https://www.nuget.org/packages/DickinsonBros.Encryption/">
    <img src="https://img.shields.io/nuget/v/DickinsonBros.Encryption">
</a>

Encrypt and Decrypt strings

Features
* Certificate based encryption 
* Configure certificate location

<a href="https://dev.azure.com/marksamdickinson/DickinsonBros/_build?definitionScope=%5CDickinsonBros.Encryption">Builds</a>

<h3>Usage</h3>

Install a windows certificate (Below you will need the ThumbPrint and StoreLocation)

<b>Create Instance</b>

<i>Code</i>

    using DickinsonBros.Encryption;
    using DickinsonBros.Encryption.Models;
    
    ...

    var encryptionServiceOptions = new EncryptionServiceOptions
    {
        ThumbPrint = "...",
        StoreLocation = "..."
    };

    var options = Options.Create(encryptionServiceOptions);
    var encryptionService = new EncryptionService(options)

<b>Create Instance (With Dependency Injeciton)</b>

<i>Add appsettings.json File With Contents</i>
    
    {
      "EncryptionSettings": {
        "ThumbPrint": "...",
        "StoreLocation": "..."
      }
    }
    
<i>Code</i>

    using DickinsonBros.Encryption.Abstractions;
    using DickinsonBros.Encryption.Extensions;
    using DickinsonBros.Encryption.Models;
    
    ...  

    var serviceCollection = new ServiceCollection();
    
    //Configure Options
    var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", false)

    var configuration = builder.Build();
    services.AddOptions();
    services.Configure<EncryptionServiceOptions>(_configuration.GetSection(nameof(EncryptionServiceOptions)));
                
    //Add Service
    serviceCollection.AddEncryptionService();

    //Build Service Provider 
    using (var provider = services.BuildServiceProvider())
    {
       var encryptionService = provider.GetRequiredService<IEncryptionService>();
    }
    
<b>Example Usage</b>

    var sampleString = "ABC123";
    var encryptedString = encryptionService.Encrypt(sampleString)
    var decryptedString = encryptionService.Decrypt(encryptedString)

