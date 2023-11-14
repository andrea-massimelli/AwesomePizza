using AwesomePizza.Models.OrderModels.PizzaTypes;
using System.ComponentModel.DataAnnotations;

namespace AwesomePizza.Models.OrderModels.Orders
{
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public PizzaType PizzaType { get; set; }

        public int PizzaQuantity { get; set; }
    }

}
