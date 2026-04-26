namespace YouScout.Infrastructure.Interfaces.Storage;

public interface IMediaStorage
{
    Task<string> UploadAsync(
        Stream fileStream,
        string fileName,
        string contentType,
        CancellationToken cancellationToken = default);


    Task DeleteAsync(
        string publicId,
        CancellationToken cancellationToken = default);


    Task<string> GetUrlAsync(
        string publicId,
        CancellationToken cancellationToken = default);
}