using FashionStore.Application.Common.DTOs;

namespace FashionStore.Application.Common.Interfaces.IServices;
public interface IFileStorageService
{
    PresignedUploadUrlDto GenerateUploadUrlAsync(string fileName, string contentType);
    Task<BlobFileInfo> GetFileInfoAsync(string blobName);
}
