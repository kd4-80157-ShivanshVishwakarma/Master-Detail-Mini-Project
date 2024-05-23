using MasterDetailProject.DTO;
using MasterDetailProject.Models;
using Microsoft.Data.SqlClient;

namespace MasterDetailProject.Repository
{
    public class OrderRepository
    {
        public OrderRepository() { }
        string ConnectionDetails = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=MasterDetail;Integrated Security=True;Pooling=False";
        
        public List<Order> ListOrders()
        {
            List<Order> orders = new List<Order>();
            using (SqlConnection connection = new SqlConnection(ConnectionDetails))
            {
                string query = "SELECT * FROM Orders";
                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Order order = new Order
                            {
                                OrderID = reader.GetInt32(0),
                                CustomerID = reader.GetInt32(1),
                                OrderDate = reader.GetDateTime(2),
                                TotalAmount = reader.GetDecimal(3)
                            };

                            orders.Add(order);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }
            return orders;
        }

        public int Add(Order value)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionDetails))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    string insertOrderQuery = @"
                    INSERT INTO Orders (CustomerID, OrderDate, TotalAmount)
                    VALUES (@CustomerID, @OrderDate, @TotalAmount);
                    SELECT SCOPE_IDENTITY();";

                    SqlCommand orderCommand = new SqlCommand(insertOrderQuery, connection, transaction);
                    orderCommand.Parameters.AddWithValue("@CustomerID", value.CustomerID);
                    orderCommand.Parameters.AddWithValue("@OrderDate", value.OrderDate);
                    orderCommand.Parameters.AddWithValue("@TotalAmount", value.TotalAmount);

                    int orderId = Convert.ToInt32(orderCommand.ExecuteScalar());
                    transaction.Commit();

                    return orderId;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("An error occurred while adding the order: " + ex.Message);
                }
            }
        }
        public bool AddOrderDetail(OrderDetail value)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionDetails))
                {
                    string query = @"INSERT INTO OrderDetails (OrderID, ProductID, Quantity,ProductName, UnitPrice) VALUES (@OrderID, @ProductID, @Quantity,@ProductName, @UnitPrice);";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@OrderID", value.OrderID);
                        command.Parameters.AddWithValue("@ProductID", value.ProductID);
                        command.Parameters.AddWithValue("@Quantity", value.Quantity);
                        command.Parameters.AddWithValue("@ProductName", value.ProductName);
                        command.Parameters.AddWithValue("@UnitPrice", value.UnitPrice);

                        connection.Open();
                        int result = command.ExecuteNonQuery();

                        return result > 0 ? true:false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred - " + ex.Message);
                return false;
            }
        }



        public OrderDetail GetOrderDetByOrdrId(int orderId)
        {
            string query = @"SELECT od.OrderID, od.Quantity, od.ProductName,  od.UnitPrice FROM OrderDetails od WHERE od.OrderID = @OrderID";
            OrderDetail? od = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionDetails))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add(new SqlParameter("@OrderID", orderId));

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                od= new OrderDetail
                                {
                                    OrderID = Convert.ToInt32(reader["OrderID"]),
                                    Quantity = Convert.ToInt32(reader["Quantity"]),
                                    ProductName = reader["ProductName"].ToString(),
                                    UnitPrice = reader.GetDecimal(reader.GetOrdinal("UnitPrice"))
                                };
                            }
                            return od;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                throw;
            }
        }
    }
}
