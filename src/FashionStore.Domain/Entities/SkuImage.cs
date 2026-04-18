namespace FashionStore.Domain.Entities;

public class SkuImage : BaseEntity
{
    public int SkuImageId { get; set; }
    public int SkuId { get; set; }
    public string Image { get; set; }

    public Sku Sku { get; set; }

}
