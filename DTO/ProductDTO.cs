using MasterDetailProject.Models;

namespace MasterDetailProject.DTO
{
    public class ProductDTO
    {
        public ProductDTO()
        {

        }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Category { get; set; }

        public static implicit operator ProductDTO(Products product)
        {
            string categoryName = GetCategoryNameById(product.CategoryID);

            return new ProductDTO
            {
                ProductName = product.ProductName,
                Price = product.Price,
                Quantity = product.Quantity,
                Category = categoryName
            };
        }

        private static string GetCategoryNameById(int categoryId)
        {
            return categoryId switch
            {
                1 => "Electronics",
                2 => "Stationary",
                3 => "FoodItems",
                4 => "Books"
            };
        }
    }
}
