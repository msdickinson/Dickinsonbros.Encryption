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

```C#
var encryptedString = encryptionService.Encrypt("Sample123!");
var decryptedString = encryptionService.Decrypt(encryptedString);
    Console.WriteLine(
$@"Encrypted String
{ encryptedString }

Decrypted String
{ decryptedString }
");
```
    
    Encrypted String
    f4KFyOSqPAM8ju2q+521D3S2zGNuvsNks382GOlgL8C3VyaWVhCF4VS0bIyoQjK8KsoI7mQ8Uu8w54TkzCHuFGqXOmLJU0Rfjurjn+01VCxBsgo1G23u4QUtM5uXBSye/S/jcXGVLDJX90F7gss+NdKvbhebq6jFnFsR6ZhrTGc7BLbLiE0M/BE7A+8hxCGjOFXvgwBm8nTFhXh/sSV8fbZ9pCzwcPuSXMTKxRi+cji3jN42hJidmOBNKIXi2pq6hIL5kcDxKXuVxznOOOcwh/clfCa8Hx6rY/q1O4y14AT5IknCnvYXWCEroXfvX1vlemXewL/UCN486c6VzGssGA==

    Decrypted String
    Sample123!

Example Runner Included in folder "DickinsonBros.Encryption.Runner"

<h2>Setup</h2>

<i>Install a windows certificate</i>

<i>Add Nuget References</i>

    https://www.nuget.org/packages/DickinsonBros.Encryption/
    https://www.nuget.org/packages/DickinsonBros.Encryption/Abstractions

<h3>Create Instance</h3>

<i>Code</i>
```c#
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

```

<h3>Create Instance (With Dependency Injection)</h3>

<i>Add appsettings.json File With Contents</i>
 ```json  
    {
      "EncryptionSettings": {
        "ThumbPrint": "...",
        "StoreLocation": "..."
      }
    }
 ```    
<i>Code</i>
```c#

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
    serviceCollection.AddOptions();
    serviceCollection.Configure<EncryptionServiceOptions>(_configuration.GetSection(nameof(EncryptionServiceOptions)));
                
    //Add Service
    serviceCollection.AddEncryptionService();
    
    //Build Service Provider 
    using (var provider = services.BuildServiceProvider())
    {
       var encryptionService = provider.GetRequiredService<IEncryptionService>();
    }
```
