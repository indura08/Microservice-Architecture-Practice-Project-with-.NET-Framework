namespace OrderService.Model
{
    public class OrderDTO
    {
        public string UserId { get; set; } = null!;
        public string ProductId { get; set; } = null!;
        public decimal TotalAmount { get; set; }
    }
}
