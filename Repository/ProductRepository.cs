using MasterDetailProject.DTO;
using MasterDetailProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MasterDetailProject.Repository
{
    public class ProductRepository
    {
        public ProductRepository() { }

        string ConnectionDetails = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=MasterDetail;Integrated Security=True;Pooling=False";


        public List<ProductDTO> GetList()
        {
            List<ProductDTO> products = new List<ProductDTO>();

            using (SqlConnection connection = new SqlConnection(ConnectionDetails))
            {
                string query = "SELECT * FROM Products";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Products product = new Products
                            {
                                ProductName = reader["ProductName"].ToString(),
                                Price = Convert.ToDecimal(reader["Price"]),
                                Quantity = Convert.ToInt32(reader["Quantity"]),
                                CategoryID = Convert.ToInt32(reader["CategoryID"])
                            };
                            products.Add(product);
                        }
                    }
                }
            }
            return products;
        }

        public bool Add(Products value)
        {
            try { 
                using (SqlConnection connection = new SqlConnection(ConnectionDetails))
                {
                    string query = "INSERT INTO Products (ProductName, Price, Quantity, CategoryID) VALUES (@ProductName, @Price, @Quantity, @CategoryID)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProductName", value.ProductName);
                        command.Parameters.AddWithValue("@Price", value.Price);
                        command.Parameters.AddWithValue("@Quantity", value.Quantity);
                        command.Parameters.AddWithValue("@CategoryID", value.CategoryID);

                        connection.Open();
                        int result = command.ExecuteNonQuery();

                        if (result > 0)
                        {
                            return true;
                        }
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public int GetProductIdByName(string name)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionDetails))
                {
                    string query = "select ProductID from Products where ProductName = @Name";
                    using (SqlCommand commnd = new SqlCommand(query, connection))
                    {
                        commnd.Parameters.Add(new SqlParameter("@Name", name));
                        connection.Open();

                        int res = (int)commnd.ExecuteScalar();
                        return res;
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occured - " + ex.Message);
                throw;

            }
        }
    }

}
