using Microsoft.Extensions.DependencyInjection;
using YouScout.Infrastructure.Interfaces.Storage;

namespace YouScout.Infrastructure.Factories;

public class MediaStorageFactory(IServiceProvider serviceProvider) : IMediaStorageFactory
{
    public IMediaStorage Get(IMediaStorageFactory.StorageProvider provider)
    {
        return serviceProvider.GetRequiredKeyedService<IMediaStorage>(provider.ToString().ToLower());
    }
}