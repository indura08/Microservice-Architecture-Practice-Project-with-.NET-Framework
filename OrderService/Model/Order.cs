namespace OrderService.Model;

public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ProductId { get; set; }
    public string UserName { get; set; } = null!;
    public string ProductName { get; set; } = null!;
    public string OrderName { get; set; } = null!;
    public DateTime OrderDate { get; set; } = DateTime.Now;
    public decimal TotalAmount { get; set; }
}
