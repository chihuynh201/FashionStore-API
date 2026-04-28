using Azure.Storage.Blobs;
using FashionStore.Application.Common.Interfaces.IServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace FashionStore.Infrastructure.Services.Azure;
public static class AzureDependencyInjection
{
    public static void AddAzureBlobStorage(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AzureSetting>(configuration.GetSection("Azure"));
        services.AddSingleton(sp =>
        {
            var options = sp.GetRequiredService<IOptions<AzureSetting>>().Value;
            return new BlobServiceClient(options.ConnectionString);
        });

        services.AddScoped<IFileStorageService, AzureBlobStorageService>();
    }
}
