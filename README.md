# Dickinsonbros.Encryption
<a href="https://www.nuget.org/packages/DickinsonBros.Encryption/">
    <img src="https://img.shields.io/nuget/v/DickinsonBros.Encryption">
</a>

Encrypt and Decrypt strings

Features
* Certificate based encryption 
* Configure certificate location

<a href="https://dev.azure.com/marksamdickinson/DickinsonBros/_build?definitionScope=%5CDickinsonBros.Encryption">Builds</a>

<h2>Example Usage</h2>

![Alt text](https://raw.githubusercontent.com/msdickinson/DickinsonBros.Encryption/develop/EncryptionSampleUsage.PNG)

![Alt text](https://raw.githubusercontent.com/msdickinson/DickinsonBros.Encryption/develop/EncryptionSampleOutput.PNG)

Example Runner Included in folder "DickinsonBros.Encryption.Runner"

<h2>Setup</h2>


<i>Install a windows certificate</i>

<i>Add Nuget References</i>

    <PackageReference Include="DickinsonBros.Encryption" Version="..." />
    <PackageReference Include="DickinsonBros.Encryption.Abstractions" Version="..." />

<b>Create Instance</b>

<i>Add Nuget References</i>



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

<b>Create Instance (With Dependency Injection)</b>

<i>Add Nuget References</i>

    <PackageReference Include="DickinsonBros.Encryption" Version="..." />
    <PackageReference Include="DickinsonBros.Encryption.Abstractions" Version="..." />

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
