using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using FashionStore.Application.Common.DTOs;
using FashionStore.Application.Common.Interfaces.IServices;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FashionStore.Infrastructure.Services.Azure;
internal class AzureBlobStorageService : IFileStorageService
{
    private readonly BlobServiceClient _blobServiceClient;
    private readonly AzureSetting _azureSettings;
    private readonly ILogger<AzureBlobStorageService> _logger;
    public AzureBlobStorageService(BlobServiceClient blobServiceClient,
        IOptions<AzureSetting> options,
        ILogger<AzureBlobStorageService> logger)
    {
        _azureSettings = options.Value;
        _blobServiceClient = blobServiceClient;
        _logger = logger;
    }

    public PresignedUploadUrlDto GenerateUploadUrlAsync(string fileName, string contentType)
    {
        try
        {
            var blobName = Guid.NewGuid().ToString();

            var containerClient = _blobServiceClient.GetBlobContainerClient(_azureSettings.ContainerName);

            var blobClient = containerClient.GetBlobClient(blobName);

            var expiry = DateTime.UtcNow.AddMinutes(10);

            var sasUri = blobClient.GenerateSasUri(
                BlobSasPermissions.Write,
                expiry);

            var fileUrl = blobClient.Uri.ToString();

            return new PresignedUploadUrlDto
            {
                UploadUrl = sasUri.ToString(),
                BlobName = blobName,
                FileUrl = blobClient.Uri.ToString(),
                Expiry = expiry
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating presigned upload URL for blob storage.");
            throw;
        }
    }

    public async Task<BlobFileInfo> GetFileInfoAsync(string blobName)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(_azureSettings.ContainerName);

        var blob = containerClient.GetBlobClient(blobName);

        if (!await blob.ExistsAsync())
            return null;

        var props = await blob.GetPropertiesAsync();

        return new BlobFileInfo
        {
            Size = props.Value.ContentLength,
            ContentType = props.Value.ContentType,
            Url = blob.Uri.ToString(),
        };
    }
}
