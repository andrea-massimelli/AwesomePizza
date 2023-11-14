using AwesomePizza.API.Controllers.CustomerControllers;
using AwesomePizza.DAL;
using AwesomePizza.Models.OrderModels.OrderRequests;
using AwesomePizza.Models.OrderModels.Orders;
using AwesomePizza.Models.OrderModels.PizzaTypes;
using AwesomePizza.Services.OrderServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomePizza.Test.OrderControllers
{
    public class CompleteOrderTest
    {
        [Fact]
        public void CompleteOrderController()
        {
            ActionResult<int> actionResult;
            int result;
            string CustomerDetail;
            List<OrderDetail> OrderDetails = new List<OrderDetail>();
            OrderDetail itemOrderDetail = new OrderDetail();

            var dbOptionsBuilder = new DbContextOptionsBuilder();
            dbOptionsBuilder.UseInMemoryDatabase("AwesomePizzaCompleteOrder");
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

                // act
                var actionResultGetOrdersQueue = orderController.GetOrdersQueue();

                // assert
                var resultGetOrdersQueue = Utility.GetObjectResultContent<OrderRequest>(actionResultGetOrdersQueue);
                Assert.Equal(1, resultGetOrdersQueue.Id);

                // act
                var actionResultInProgressOrder = orderController.InProgressOrder(resultGetOrdersQueue.Id);

                // assert
                Assert.IsType<OkObjectResult>(actionResultInProgressOrder);

                // act
                var actionResultCompleteOrder = orderController.CompleteOrder(resultGetOrdersQueue.Id);

                // assert
                Assert.IsType<OkObjectResult>(actionResultCompleteOrder);

                // act
                var actionResultGetOrdersQueue2 = orderController.GetOrdersQueue();

                // assert
                Assert.IsType<NotFoundObjectResult>(actionResultGetOrdersQueue2.Result);
            }
        }
    }
}
