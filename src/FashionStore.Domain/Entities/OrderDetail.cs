namespace FashionStore.Domain.Entities;

public class OrderDetail : BaseEntity
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public Order Order { get; set; }
    public int SkuId { get; set; }
    public Sku Sku { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
}
