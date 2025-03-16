using OrderService.Model;

namespace OrderService.Data.Services
{
    public interface IOrderService
    {
        Task<Order> GetOredrById(int id);
        Task AddOrder(OrderDTO newOrder);
        Task<string> DeleteOrder(int id);
    }
}
