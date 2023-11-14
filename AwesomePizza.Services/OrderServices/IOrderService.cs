using AwesomePizza.Models.OrderModels.OrderRequests;
using AwesomePizza.Models.OrderModels.Orders;

namespace AwesomePizza.Services.OrderServices
{
    public interface IOrderService
    {
        public int PlaceOrder(List<OrderDetail> OrderDetail, string CustomerDetail);

        public OrderRequest? GetOrdersQueue();

        public List<OrderDetail>? GetOrderDetails(int orderId);

        public bool SetOrderStatusToInProgress(int orderId);

        public bool SetOrderStatusToCompleted(int orderId);
    }
}
