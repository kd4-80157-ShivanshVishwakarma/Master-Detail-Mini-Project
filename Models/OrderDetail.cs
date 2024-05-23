﻿namespace MasterDetailProject.Models
{
    public class OrderDetail
    {
        public OrderDetail()
        {
        }

        public int OrderDetailID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
