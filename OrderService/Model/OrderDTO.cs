namespace OrderService.Model
{
    public class OrderDTO
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public string OrderName { get; set; } = null!;
        public decimal TotalAmount { get; set; }
    }
}
