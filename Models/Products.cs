namespace MasterDetailProject.Models
{
    public class Products
    {

        public Products()
        {

        }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int CategoryID { get; set; }

        
    }
}
