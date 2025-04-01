namespace OrderService.Model;

public class Order
{
    public int Id { get; set; }
    public string UserId { get; set; } = null!;
    public string ProductId { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string ProductName { get; set; } = null!;
    public DateTime OrderDate { get; set; } = DateTime.Now;
    public decimal TotalAmount { get; set; }
    public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
}
