namespace FashionStore.Domain.Entities;

public class Sku : BaseEntity
{
    public int SkuId { get; set; }
    public string SkuCode { get; set; }
    public string Barcode { get; set; }
    public int StockQuantity { get; set; }
    public int ProductId { get; set; }

    public Product Product { get; set; }
    public ICollection<SkuAttributeValue> SkuAttributeValues { get; set; }
    public ICollection<SkuImage> SkuImages { get; set; }
    public ICollection<OrderDetail> OrderDetails { get; set; }

}
