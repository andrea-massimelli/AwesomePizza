using AwesomePizza.API.Controllers.CustomerControllers;
using AwesomePizza.DAL;
using AwesomePizza.Models.OrderModels.OrderRequests;
using AwesomePizza.Models.OrderModels.Orders;
using AwesomePizza.Models.OrderModels.OrderStatus;
using AwesomePizza.Models.OrderModels.PizzaTypes;
using AwesomePizza.Services.OrderServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace AwesomePizza.Test.OrderControllers
{
    public class GetOrdersQueueTest
    {
        //[Fact]
        //public void GetOrdersQueueService()
        //{
        //    ActionResult<int> actionResult;
        //    int result;
        //    string CustomerDetail;
        //    List<OrderDetail> OrderDetails = new List<OrderDetail>();
        //    OrderDetail itemOrderDetail = new OrderDetail();

        //    var dbOptionsBuilder = new DbContextOptionsBuilder();
        //    dbOptionsBuilder.UseInMemoryDatabase("AwesomePizza");
        //    dbOptionsBuilder.ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));

        //    // arrange
        //    using (var db = new MyDbContext(dbOptionsBuilder.Options))
        //    {
        //        // create the service
        //        var service = new OrderService(db);

        //        // act
        //        CustomerDetail = "customer detail 1";
        //        OrderDetails = new List<OrderDetail>();
        //        itemOrderDetail = new OrderDetail
        //        {
        //            PizzaType = PizzaType.Margherita,
        //            PizzaQuantity = 4,
        //        };
        //        OrderDetails.Add(itemOrderDetail);
        //        result = service.PlaceOrder(OrderDetails, CustomerDetail);

        //        // assert
        //        Assert.Equal(1, result);

        //        Thread.Sleep(500);

        //        // act
        //        CustomerDetail = "customer detail 2";
        //        OrderDetails = new List<OrderDetail>();
        //        itemOrderDetail = new OrderDetail
        //        {
        //            PizzaType = PizzaType.Marinara,
        //            PizzaQuantity = 1,
        //        };
        //        OrderDetails.Add(itemOrderDetail);
        //        result = service.PlaceOrder(OrderDetails, CustomerDetail);

        //        // assert
        //        Assert.Equal(2, result);

        //        // act
        //        var resultGetOrdersQueue = service.GetOrdersQueue();

        //        // assert
        //        Assert.Equal(2, resultGetOrdersQueue.Id);
        //        Assert.Equal(CustomerDetail, resultGetOrdersQueue.CustomerDetail);
        //        Assert.Equal(OrderStatus.Pending, resultGetOrdersQueue.OrderStatus);
        //    }
        //}

        [Fact]
        public void GetOrdersQueueController()
        {
            ActionResult<int> actionResult;
            int result;
            string CustomerDetail;
            List<OrderDetail> OrderDetails = new List<OrderDetail>();
            OrderDetail itemOrderDetail = new OrderDetail();

            var dbOptionsBuilder = new DbContextOptionsBuilder();
            dbOptionsBuilder.UseInMemoryDatabase("AwesomePizzaGetOrdersQueue");
            dbOptionsBuilder.ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));

            // arrange
            using (var db = new MyDbContext(dbOptionsBuilder.Options))
            {
                // create the service
                var service = new OrderService(db);

                OrderController orderController = new OrderController(service);

                // act
                CustomerDetail = "customer detail 1";
                OrderDetails = new List<OrderDetail>();
                itemOrderDetail = new OrderDetail
                {
                    PizzaType = PizzaType.Margherita,
                    PizzaQuantity = 4,
                };
                OrderDetails.Add(itemOrderDetail);
                actionResult = orderController.PlaceOrder(OrderDetails, CustomerDetail);

                // assert
                result = Utility.GetObjectResultContent(actionResult);
                Assert.Equal(1, result);

                Thread.Sleep(1000);

                // act
                CustomerDetail = "customer detail 2";
                OrderDetails = new List<OrderDetail>();
                itemOrderDetail = new OrderDetail
                {
                    PizzaType = PizzaType.Marinara,
                    PizzaQuantity = 1,
                };
                OrderDetails.Add(itemOrderDetail);
                actionResult = orderController.PlaceOrder(OrderDetails, CustomerDetail);

                // assert
                result = Utility.GetObjectResultContent(actionResult);
                Assert.Equal(2, result);

                // act
                var actionResultGetOrdersQueue = orderController.GetOrdersQueue();

                // assert
                var resultGetOrdersQueue = Utility.GetObjectResultContent(actionResultGetOrdersQueue);
                Assert.Equal(1, resultGetOrdersQueue.Id);
                Assert.Equal("customer detail 1", resultGetOrdersQueue.CustomerDetail);
                Assert.Equal(OrderStatus.Pending, resultGetOrdersQueue.OrderStatus);
            }
        }
    }
}
