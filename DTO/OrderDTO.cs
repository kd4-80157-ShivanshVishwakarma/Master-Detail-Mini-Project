namespace MasterDetailProject.DTO
{
    public class OrderDTO
    {
        public OrderDTO()
        {

        }

        public int CustomerID { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
