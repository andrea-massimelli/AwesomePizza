using AwesomePizza.API.Controllers.CustomerControllers;
using AwesomePizza.DAL;
using AwesomePizza.Models.OrderModels.Orders;
using AwesomePizza.Models.OrderModels.PizzaTypes;
using AwesomePizza.Services.OrderServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace AwesomePizza.Test.OrderControllers
{
    public class PlaceOrderTest
    {
        //[Fact]
        //public void PlaceOrderService()
        //{
        //    int result;
        //    string CustomerDetail = "customer 01";
        //    List<OrderDetail> OrderDetails = new List<OrderDetail>();
        //    OrderDetail itemOrderDetail = new OrderDetail();

        //    itemOrderDetail = new OrderDetail
        //    {
        //        PizzaType = PizzaType.Margherita,
        //        PizzaQuantity = 1,
        //    };
        //    OrderDetails.Add(itemOrderDetail);

        //    itemOrderDetail = new OrderDetail
        //    {
        //        PizzaType = PizzaType.Diavola,
        //        PizzaQuantity = 2,
        //    };
        //    OrderDetails.Add(itemOrderDetail);

        //    itemOrderDetail = new OrderDetail
        //    {
        //        PizzaType = PizzaType.Marinara,
        //        PizzaQuantity = 1,
        //    };
        //    OrderDetails.Add(itemOrderDetail);

        //    var dbOptionsBuilder = new DbContextOptionsBuilder();
        //    dbOptionsBuilder.UseInMemoryDatabase("AwesomePizza");
        //    dbOptionsBuilder.ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));

        //    // using Moq as the mocking library
        //    var orderService = new Mock<IOrderService>();

        //    // arrange
        //    using (var db = new MyDbContext(dbOptionsBuilder.Options))
        //    {
        //        // create the service
        //        var service = new OrderService(db);

        //        // act
        //        result = service.PlaceOrder(OrderDetails, CustomerDetail);

        //        // assert
        //        Assert.NotEqual(-1, result);
        //        Assert.Equal(1, result);

        //        // act
        //        result = service.PlaceOrder(OrderDetails, CustomerDetail);

        //        // assert
        //        Assert.NotEqual(-1, result);
        //        Assert.Equal(2, result);
        //    }
        //}

        [Fact]
        public void PlaceOrderController()
        {
            ActionResult<int> actionResult;
            int result;
            string CustomerDetail = "customer 01";
            List<OrderDetail> OrderDetails = new List<OrderDetail>();
            OrderDetail itemOrderDetail = new OrderDetail();

            itemOrderDetail = new OrderDetail
            {
                PizzaType = PizzaType.Margherita,
                PizzaQuantity = 1,
            };
            OrderDetails.Add(itemOrderDetail);

            itemOrderDetail = new OrderDetail
            {
                PizzaType = PizzaType.Diavola,
                PizzaQuantity = 2,
            };
            OrderDetails.Add(itemOrderDetail);

            itemOrderDetail = new OrderDetail
            {
                PizzaType = PizzaType.Marinara,
                PizzaQuantity = 1,
            };
            OrderDetails.Add(itemOrderDetail);

            var dbOptionsBuilder = new DbContextOptionsBuilder();
            dbOptionsBuilder.UseInMemoryDatabase("AwesomePizzaPlaceOrder");
            dbOptionsBuilder.ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));

            // arrange
            using (var db = new MyDbContext(dbOptionsBuilder.Options))
            {
                // create the service
                var service = new OrderService(db);

                OrderController orderController = new OrderController(service);

                // act
                actionResult = orderController.PlaceOrder(OrderDetails, CustomerDetail);

                // assert
                result = Utility.GetObjectResultContent(actionResult);
                Assert.Equal(1, result);

                // act
                actionResult = orderController.PlaceOrder(OrderDetails, CustomerDetail);

                // assert
                result = Utility.GetObjectResultContent(actionResult);
                Assert.Equal(2, result);
            }
        }
    }
}