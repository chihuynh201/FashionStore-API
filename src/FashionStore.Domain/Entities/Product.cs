namespace FashionStore.Domain.Entities;

public class Product : BaseEntity
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Thumbnail { get; set; }
    public int CategoryId { get; set; }
    public bool IsEnabled { get; set; }
    public bool IsDeleted { get; set; }

    public Category Category { get; set; }
    public ICollection<Sku> Skus { get; set; }

}
