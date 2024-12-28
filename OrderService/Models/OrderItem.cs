using EMall.Shared;

namespace OrderService.Models
{
    public class OrderItem
    {
        public Int32 ID { get; set; }
        public Int32 ProductID { get; set; }
        //public Product Product { get; set; }
        public Int32 Quantity { get; set; }
        public Decimal Price { get; set; }
    }
}
