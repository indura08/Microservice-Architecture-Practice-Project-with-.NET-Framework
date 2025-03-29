using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderService.Data.Services;
using OrderService.Model;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderByID(int id)
        {
            var currentOrder = await _orderService.GetOredrById(id);
            if (currentOrder != null)
            {
                return Ok(currentOrder);
            }
            else return NotFound(new { message = "Order Not Found" });
        }

        [HttpPost("create")]
        public async Task<ActionResult<string>> CreateNewOrder(OrderDTO orderDTO)
        {
            await _orderService.AddOrder(orderDTO);
            return Ok($"Your order {orderDTO.OrderName} Has been successfully added");
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<string>> DeleteORderById(int id)
        {
            var status = await _orderService.DeleteOrder(id);
            if (status.ToString() == "Done") return Ok("Order deleted!");
            else return NotFound($"Order Could not found!");
        }
    }
}
