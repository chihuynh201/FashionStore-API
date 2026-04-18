namespace FashionStore.Domain.Entities;

public class CategoryAttribute : BaseEntity
{
    public int CategoryAttributeId { get; set; }
    public int CategoryId { get; set; }
    public int ProductAttributeId { get; set; }

    public Category Category { get; set; }
    public ProductAttribute ProductAttribute { get; set; }

}
