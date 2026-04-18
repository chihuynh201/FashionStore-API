namespace FashionStore.Domain.Entities;

public class Customer : BaseEntity
{
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
    public string Phone { get; set; }

    public ICollection<Order> Orders { get; set; }
}
