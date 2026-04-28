namespace FashionStore.Application.Common.DTOs;
public class PresignedUploadUrlDto
{
    public string UploadUrl { get; set; }
    public string BlobName { get; set; }
    public string FileUrl { get; set; }
    public DateTime Expiry { get; set; }
}
