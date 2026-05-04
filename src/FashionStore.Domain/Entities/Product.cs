namespace FashionStore.Domain.Entities;

public class Product : BaseEntity
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int? FileId { get; set; }
    public int CategoryId { get; set; }
    public bool IsEnabled { get; set; }
    public bool IsDeleted { get; set; }

    public Category Category { get; set; }
    public FileUpload File { get; set; }
    public ICollection<Sku> Skus { get; set; }
    public void Deactivate() => IsEnabled = false;
    public void Activate() => IsEnabled = true;

}
