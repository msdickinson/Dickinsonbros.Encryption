using DickinsonBros.Encryption.Abstractions;
using DickinsonBros.Encryption.Models;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace DickinsonBros.Encryption
{
    [ExcludeFromCodeCoverage]
    public class EncryptionService : IEncryptionService
    {
        internal readonly string _thumbPrint;
        internal readonly StoreLocation _storeLocation;

        public EncryptionService(IOptions<EncryptionServiceOptions> encryptionServiceOptions)
        {
            _thumbPrint = encryptionServiceOptions.Value.ThumbPrint;
            _storeLocation = encryptionServiceOptions.Value.StoreLocation == "LocalMachine"
                                ? StoreLocation.LocalMachine : StoreLocation.CurrentUser;
        }

        /// <summary>
        /// Takes encrypted string and decrypts it.
        /// </summary>
        /// <param name="encryptedString"></param>
        /// <returns>decrypted string</returns>
        public string Decrypt(string encryptedString)
        {
            try
            {
                using (var x509Store = new X509Store(StoreName.My, _storeLocation))
                {
                    x509Store.Open(OpenFlags.ReadOnly);
                    var certificateCollection = x509Store.Certificates.Find(X509FindType.FindByThumbprint, _thumbPrint, false);
                    if (certificateCollection.Count > 0)
                    {
                        var certificate = certificateCollection[0];
                        using (var rsaPrivateKey = certificate.GetRSAPrivateKey())
                        {
                            return Encoding.ASCII.GetString(rsaPrivateKey.Decrypt(Convert.FromBase64String(encryptedString), RSAEncryptionPadding.Pkcs1));
                        }
                    }
                    else
                    {
                        throw new Exception($"No certificate found for Thumbprint {_thumbPrint} in location {_storeLocation}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Unhandled exception. Thumbprint: {_thumbPrint}, Location: {_storeLocation}", ex);
            }
        }

        /// <summary>
        /// Takes string and encrypts it.
        /// </summary>
        /// <param name="encryptedString"></param>
        /// <returns>Encrypted string</returns>
        public string Encrypt(string rawString)
        {
            try
            {
                using (var x509Store = new X509Store(StoreName.My, _storeLocation))
                {
                    x509Store.Open(OpenFlags.ReadOnly);
                    var certificateCollection = x509Store.Certificates.Find(X509FindType.FindByThumbprint, _thumbPrint, false);
                    if (certificateCollection.Count > 0)
                    {
                        var certificate = certificateCollection[0];
                        using (RSA rsa = certificate.GetRSAPrivateKey())
                        {
                            byte[] bytestodecrypt = Encoding.UTF8.GetBytes(rawString);
                            byte[] plainbytes = rsa.Encrypt(bytestodecrypt, RSAEncryptionPadding.Pkcs1);
                            return Convert.ToBase64String(plainbytes);
                        }
                    }
                    else
                    {
                        throw new Exception($"No certificate found for Thumbprint {_thumbPrint} in location {_storeLocation}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Unhandled exception. Thumbprint: {_thumbPrint}, Location: {_storeLocation}", ex);
            }
        }
    }

}
