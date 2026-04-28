namespace FashionStore.Domain.Entities;
public class FileUpload : BaseEntity
{
    public int FileId { get; set; }
    public string FileName { get; set; }
    public string Url { get; set; }
    public string ContentType { get; set; }
    public long Size { get; set; }
    public string Status { get; set; }
}
