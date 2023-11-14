using AwesomePizza.Models.OrderModels.OrderRequests;
using AwesomePizza.Models.OrderModels.Orders;
using AwesomePizza.Services.OrderServices;
using Microsoft.AspNetCore.Mvc;

namespace AwesomePizza.API.Controllers.CustomerControllers
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

        // API to place order
        [HttpPost("placeorder")]
        public ActionResult<int> PlaceOrder([FromBody] List<OrderDetail> OrderDetail, string CustomerDetail)
        {
            var orderId = _orderService.PlaceOrder(OrderDetail, CustomerDetail);

            if (orderId == -1)
            {
                return NotFound("Order not placed.");
            }
            else
            {
                return Ok(orderId);
            }
        }

        // API to obtain the first order avaiable
        [HttpGet("getorderqueque")]
        public ActionResult<OrderRequest> GetOrdersQueue()
        {
            var pendingOrder = _orderService.GetOrdersQueue();

            if (pendingOrder == null)
            {
                return NotFound("Order pending not found.");
            }
            else
            {
                return Ok(pendingOrder);
            }
        }

        // API to obtain order details
        [HttpGet("getorderdetails/{orderId}")]
        public ActionResult<List<OrderDetail>> GetOrderDetails(int orderId)
        {
            var orderDetails = _orderService.GetOrderDetails(orderId);

            if (orderDetails == null)
            {
                return NotFound("Order details not found.");
            }
            else
            {
                return Ok(orderDetails);
            }
        }

        // API to set order status in progress
        [HttpPut("inprogress/{orderId}")]
        public ActionResult InProgressOrder(int orderId)
        {
            var inProgressOrder = _orderService.SetOrderStatusToInProgress(orderId);

            if (inProgressOrder == false)
            {
                return NotFound("Order not found or not pending.");
            }
            else
            {
                return Ok($"Order {orderId} in progress.");
            }
        }

        // API to set order status completed
        [HttpPut("complete/{orderId}")]
        public ActionResult CompleteOrder(int orderId)
        {
            var inProgressOrder = _orderService.SetOrderStatusToCompleted(orderId);

            if (inProgressOrder == false)
            {
                return NotFound("Order not found or not in progress.");
            }
            else
            {
                return Ok($"Order {orderId} completed.");
            }
        }
    }
}
