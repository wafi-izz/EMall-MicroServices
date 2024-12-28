namespace OrderService.Models
{
    public class Order
    {
        public Int32 ID { get; set; }
        public String CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public Decimal TotalPrice { get; set; }
        public List<OrderItem> OrderItem { get; set; }
    }
}
