namespace YouScout.Infrastructure.Interfaces.Storage;

public interface IMediaStorageFactory
{
    public enum StorageProvider
    {
        Local,
        Cloudinary,
        S3
    }

    IMediaStorage Get(StorageProvider provider);
}