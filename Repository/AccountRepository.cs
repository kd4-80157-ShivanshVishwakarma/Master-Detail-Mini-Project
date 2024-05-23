using MasterDetailProject.Models;
using Microsoft.Data.SqlClient;

namespace MasterDetailProject.Repository
{
    public class AccountRepository
    {
        public AccountRepository() { }
        
        string ConnectionDetails = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=MasterDetail;Integrated Security=True;Pooling=False";
        
        public bool SignUp(string username, string password,string email,string phone,string address)
        {
            
            SqlConnection connection = new SqlConnection(ConnectionDetails);
            connection.Open();

            string query = "insert into Customers(Name,Email,Password,Phone,Address) values(@Name,@Email,@Password,@Phone,@Address)";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.Add(new SqlParameter("@Name", username));
            command.Parameters.Add(new SqlParameter("@Email", email));
            command.Parameters.Add(new SqlParameter("@Password", password));
            command.Parameters.Add(new SqlParameter("@Phone", phone));
            command.Parameters.Add(new SqlParameter("@Address", address));

            int rowsAffected = command.ExecuteNonQuery();
            connection.Close();

            if (rowsAffected == 1)
            {
                //Console.WriteLine("Rows Affected : "+rowsAffected);
                return true;
            }
            else
            {
                //Console.WriteLine("data not entered correctly !!!");
                return false;

            }
        }

        public bool SignIn(Login value)
        {
            string query = "SELECT COUNT(1) FROM Customers WHERE Email = @Email AND Password = @Password";

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionDetails))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", value.Email);
                        command.Parameters.AddWithValue("@Password", value.Password);

                        connection.Open();

                        int count = Convert.ToInt32(command.ExecuteScalar());

                        return count > 0 ? true : false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
