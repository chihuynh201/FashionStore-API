namespace FashionStore.Domain.Entities;

public class SkuAttributeValue : BaseEntity
{
    public int SkuAttributeValueId { get; set; }
    public int SkuId { get; set; }
    public int AttributeValueId { get; set; }

    public Sku Sku { get; set; }
    public AttributeValue AttributeValue { get; set; }

}
