namespace FashionStore.Domain.Entities;

public class AttributeValue : BaseEntity
{
    public int AttributeValueId { get; set; }
    public string Value { get; set; }
    public int DisplayOrder { get; set; }
    public int ProductAttributeId { get; set; }

    public ProductAttribute ProductAttribute { get; set; }
    public ICollection<SkuAttributeValue> SkuAttributeValues { get; set; }

}
