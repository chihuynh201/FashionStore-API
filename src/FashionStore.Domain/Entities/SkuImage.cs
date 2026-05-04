namespace FashionStore.Domain.Entities;

public class SkuImage : BaseEntity
{
    public int SkuImageId { get; set; }
    public int SkuId { get; set; }
    public int FileId { get; set; }

    public Sku Sku { get; set; }
    public FileUpload File { get; set; }

}
