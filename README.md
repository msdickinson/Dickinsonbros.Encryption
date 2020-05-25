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

    var telemetryServiceOptions = new TelemetryServiceOptions
    {
        ThumbPrint = "...",
        StoreLocation = "..."
    };

    var options = Options.Create(telemetryServiceOptions);
    var encryptionService = new EncryptionService(options)

<b>Create Instance (With Depdency Injeciton)</b>

<i>Add appsettings.json File With Contents</i>
    
    {
      "EncryptionSettings": {
        "ThumbPrint": "...",
        "StoreLocation": "..."
      }
    }
    
<i>Add Code</i>

    var services = new ServiceCollection();
    var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", false)
        
    services.Configure<EncryptionSettings>(configuration.GetSection("EncryptionSettings"));
    services.AddSingleton<IEncryptionService, EncryptionService>();
    
    using (var provider = services.BuildServiceProvider())
    {
       var encryptionService = provider.GetRequiredService<IEncryptionService>();
    }
    
<b>Example Usage</b>

    var sampleString = "ABC123";
    var encryptedString = encryptionService.Encrypt(sampleString)
    var decryptedString = encryptionService.Decrypt(encryptedString)

