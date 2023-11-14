using AwesomePizza.DAL;
using AwesomePizza.Models.OrderModels.OrderRequests;
using AwesomePizza.Models.OrderModels.Orders;
using AwesomePizza.Models.OrderModels.OrderStatus;

namespace AwesomePizza.Services.OrderServices
{
    public class OrderService : IOrderService
    {
        private readonly MyDbContext _context;

        public OrderService(MyDbContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        public int PlaceOrder(List<OrderDetail> OrderDetails, string CustomerDetail)
        {
            int orderId = -1;

            try
            {
                using (var dbContextTransaction = _context.Database.BeginTransaction())
                {
                    //Save new order request
                    OrderRequest newOrder = new OrderRequest
                    {
                        OrderDate = DateTime.Now,
                        CustomerDetail = CustomerDetail,
                        OrderStatus = OrderStatus.Pending,
                    };
                    _context.OrderRequests.Add(newOrder);

                    //Assign the id of the order request to the order details
                    foreach (var orderDetail in OrderDetails) 
                    {
                        orderDetail.OrderId = newOrder.Id;
                        _context.OrderDetails.Add(orderDetail);
                    }                    

                    dbContextTransaction.Commit();

                    orderId = newOrder.Id;
                }

                return orderId;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        // Get the first order pending with older date
        public OrderRequest? GetOrdersQueue()
        {
            try
            {
                var pendingOrder = _context.OrderRequests.Local.Where(w => w.OrderStatus == OrderStatus.Pending).OrderBy(o => o.OrderDate).FirstOrDefault();

                return pendingOrder;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<OrderDetail>? GetOrderDetails(int orderId)
        {
            try
            {
                var orderDetails = _context.OrderDetails.Local.Where(w => w.OrderId == orderId).ToList();

                return orderDetails;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool SetOrderStatusToInProgress(int orderId)
        {
            var pendingOrder = _context.OrderRequests.Local.Where(w => w.Id == orderId && w.OrderStatus == OrderStatus.Pending).FirstOrDefault();

            if (pendingOrder == null)
            {
                return false;
            }
            else
            {
                try
                {
                    pendingOrder.OrderStatus = OrderStatus.InProgress;
                    _context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool SetOrderStatusToCompleted(int orderId)
        {
            var inProgressOrder = _context.OrderRequests.Local.Where(w => w.Id == orderId && w.OrderStatus == OrderStatus.InProgress).FirstOrDefault();

            if (inProgressOrder == null)
            {
                return false;
            }
            else
            {
                try
                {
                    inProgressOrder.OrderStatus = OrderStatus.Completed;
                    _context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}
