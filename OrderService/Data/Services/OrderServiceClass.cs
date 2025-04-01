using Microsoft.EntityFrameworkCore;
using OrderService.Model;

namespace OrderService.Data.Services
{
    public class OrderServiceClass : IOrderService
    {
        private readonly AppDbContext _dbContext;
        private readonly UserClient _userClient;
        private readonly ProductClient _productClient;

        public OrderServiceClass(AppDbContext dbContext, UserClient userClient, ProductClient productClient)
        {
            _dbContext = dbContext;
            _userClient = userClient;
            _productClient = productClient;
        }

        public async Task AddOrder(OrderDTO orderDTO)
        {
            var userDTO = await _userClient.GetUserById(orderDTO.UserId);
            var productDTO = await _productClient.GetProductByID(orderDTO.ProductId);

            if (userDTO != null && productDTO != null)
            {
                Order newOrder = new Order
                {
                    UserId = orderDTO.UserId,
                    ProductId = orderDTO.ProductId,
                    UserName = userDTO.UserName,
                    ProductName = productDTO.ProductName,
                    OrderDate = DateTime.Now,
                    TotalAmount = orderDTO.TotalAmount
                };

                _dbContext.Orders.Add(newOrder);
                await _dbContext.SaveChangesAsync();
            }
            //meke error handling hari nm krnna one , but practice project ekk hinda minimal widiyt krna giya error handling nokara
        }

        public async Task<string> DeleteOrder(int id)
        {
            var currentOrder = await GetOredrById(id);
            if (currentOrder != null)
            {
                await _dbContext.Orders.Where(order => order.Id == id).ExecuteDeleteAsync();
                return "Done";
            }
            else return "NotFound";
        }

        public async Task<Order> GetOredrById(int id)
        {
            var currentOrder = await _dbContext.Orders.FindAsync(id);
            if (currentOrder != null)
            {
                return currentOrder;
            }
            else return null!;
        }
    }
}
