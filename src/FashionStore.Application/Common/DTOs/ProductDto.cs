namespace FashionStore.Application.Common.DTOs;

public class ProductDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Thumbnail { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public bool IsEnabled { get; set; }
}
