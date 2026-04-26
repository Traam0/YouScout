using System.Net;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using YouScout.Infrastructure.Interfaces.Storage;

namespace YouScout.Infrastructure.Storage.Cloudinary;

public class CloudinaryMediaStorage : IMediaStorage
{
    private readonly CloudinaryDotNet.Cloudinary _cloudinary;

    public CloudinaryMediaStorage(IOptions<CloudinarySettings> config)
    {
        var account = new Account(config.Value.CloudName, config.Value.ApiKey, config.Value.ApiSecret);
        this._cloudinary = new CloudinaryDotNet.Cloudinary(account);
    }

    public async Task<string> UploadAsync(Stream fileStream, string fileName, string contentType,
        CancellationToken cancellationToken = default)
    {
        if (fileStream == null || fileStream.Length == 0) throw new ArgumentNullException(nameof(fileStream));
        RawUploadParams @params = new()
        {
            File = new FileDescription(fileName, fileStream),
            PublicId = Guid.NewGuid().ToString("D"),
            Overwrite = true
        };
        UploadResult result;

        if (contentType.StartsWith("image"))
        {
            var imageParams = new ImageUploadParams
            {
                File = new FileDescription(fileName, fileStream),
                PublicId = Path.GetFileNameWithoutExtension(fileName),
                Overwrite = true
            };

            result = await _cloudinary.UploadAsync(imageParams, cancellationToken);
        }
        else
        {
            result = await _cloudinary.UploadAsync((VideoUploadParams)@params, cancellationToken);
        }

        return result.StatusCode != HttpStatusCode.OK
            ? throw new Exception($"Upload failed: {result.Error?.Message}")
            : result.SecureUrl.ToString();
    }

    public Task DeleteAsync(string publicId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetUrlAsync(string publicId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}