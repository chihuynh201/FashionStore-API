namespace FashionStore.Domain.Entities;

public class Category : BaseEntity
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public int? ParentId { get; set; }
    public bool IsEnabled { get; set; }
    public bool IsDeleted { get; set; }

    public Category Parent { get; set; }
    public ICollection<Category> SubCategories { get; set; }
    public ICollection<Product> Products { get; set; }
    public ICollection<CategoryAttribute> CategoryAttributes { get; set; }

}
