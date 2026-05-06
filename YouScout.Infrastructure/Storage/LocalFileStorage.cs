using Microsoft.AspNetCore.Hosting;
using System.Text.RegularExpressions;
using YouScout.Infrastructure.Interfaces.Storage;

namespace YouScout.Infrastructure.Storage;

public partial class LocalFileStorage : IMediaStorage
{
    private readonly string _rootPath;
    private readonly string _baseUrl;

    public LocalFileStorage(IWebHostEnvironment env)
    {
        _rootPath = Path.Combine(env.WebRootPath, "uploads");
        _baseUrl = "/uploads";

        Directory.CreateDirectory(_rootPath);
    }

    public async Task<string> UploadAsync(
        Stream fileStream,
        string fileName,
        string contentType,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(fileName))
            throw new ArgumentException("Invalid file name.");

        var sanitizedFileName = SanitizeFileName(fileName);

        var extension = Path.GetExtension(sanitizedFileName);
        if (string.IsNullOrWhiteSpace(extension))
            throw new InvalidOperationException("File must have an extension.");

        var publicId = $"{Guid.NewGuid()}{extension}";

        var filePath = Path.Combine(_rootPath, publicId);

        await using var fileStreamOutput = new FileStream(filePath, FileMode.Create);
        await fileStream.CopyToAsync(fileStreamOutput, cancellationToken);

        return publicId.Replace("\\", "/");
    }

    public Task DeleteAsync(
        string publicId,
        CancellationToken cancellationToken = default)
    {
        var filePath = Path.Combine(_rootPath, publicId);

        if (File.Exists(filePath))
            File.Delete(filePath);

        return Task.CompletedTask;
    }

    public Task<string> GetUrlAsync(
        string publicId,
        CancellationToken cancellationToken = default)
    {
        var url = $"{_baseUrl}/{publicId}";
        return Task.FromResult(url.Replace("\\", "/"));
    }

    private static string SanitizeFileName(string fileName)
    {
        fileName = Path.GetFileName(fileName);

        fileName = FormatFileName().Replace(fileName, "_");

        return fileName.Length > 100
            ? fileName[..100]
            : fileName;
    }

    [GeneratedRegex(@"[^a-zA-Z0-9\.\-_]")]
    private static partial Regex FormatFileName();
}