namespace FashionStore.Application.Common.DTOs;

public class CategoryDto
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public int? ParentId { get; set; }
    public string Path { get; set; }
    public int Level { get; set; }
    public int ProductCount { get; set; }
}
