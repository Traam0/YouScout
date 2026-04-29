using Microsoft.AspNetCore.Hosting;
using System.Text.RegularExpressions;
using YouScout.Infrastructure.Interfaces.Storage;

namespace YouScout.Infrastructure.Storage;

public class LocalFileStorage : IMediaStorage
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
        // 1. Validate basic type (optional but recommended)
        if (string.IsNullOrWhiteSpace(fileName))
            throw new ArgumentException("Invalid file name.");

        // 2. Sanitize file name
        var sanitizedFileName = SanitizeFileName(fileName);

        // 3. Extract extension safely
        var extension = Path.GetExtension(sanitizedFileName);
        if (string.IsNullOrWhiteSpace(extension))
            throw new InvalidOperationException("File must have an extension.");

        // 4. Organize into folders (by date)
        var folder = Path.Combine(
            DateTime.UtcNow.Year.ToString(),
            DateTime.UtcNow.Month.ToString("D2")
        );

        var fullFolderPath = Path.Combine(_rootPath, folder);
        Directory.CreateDirectory(fullFolderPath);

        // 5. Generate unique public ID
        var publicId = $"{folder}/{Guid.NewGuid()}{extension}";

        // 6. Final file path (safe)
        var filePath = Path.Combine(_rootPath, publicId);

        // 7. Save file
        using var fileStreamOutput = new FileStream(filePath, FileMode.Create);
        await fileStream.CopyToAsync(fileStreamOutput, cancellationToken);

        return publicId.Replace("\\", "/"); // normalize for URLs
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

    // 🔐 File name sanitization
    private static string SanitizeFileName(string fileName)
    {
        // Remove path traversal attempts
        fileName = Path.GetFileName(fileName);

        // Replace invalid characters
        fileName = Regex.Replace(fileName, @"[^a-zA-Z0-9\.\-_]", "_");

        // Limit length (optional safety)
        return fileName.Length > 100
            ? fileName.Substring(0, 100)
            : fileName;
    }
}