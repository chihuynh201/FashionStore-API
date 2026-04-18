namespace FashionStore.Domain.Entities;

public class ProductAttribute : BaseEntity
{
    public int ProductAttributeId { get; set; }
    public string AttributeName { get; set; }

    public ICollection<AttributeValue> AttributeValues { get; set; }
    public ICollection<CategoryAttribute> CategoryAttributes { get; set; }

}
