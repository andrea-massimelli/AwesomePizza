using System.ComponentModel.DataAnnotations.Schema;

namespace AwesomePizza.Models.OrderModels.OrderRequests
{
    public class OrderRequest
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string? CustomerDetail { get; set; }
        public OrderStatus.OrderStatus OrderStatus { get; set; }
    }
}
