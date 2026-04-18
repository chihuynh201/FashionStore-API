namespace FashionStore.Domain.Entities;

public class Order : BaseEntity
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }

    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public ICollection<OrderDetail> OrderDetails { get; set; }
}
