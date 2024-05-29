using System.Data;
using System.Data.SqlClient;

namespace Order_System.Models
{
    public class Data
    {
        public Response register (Users users, SqlConnection connection)
        {
            try
            {
                Response response = new Response();
                SqlCommand cmd = new SqlCommand("sql_register", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FirstName", users.FirstName);
                cmd.Parameters.AddWithValue("@LastName", users.LastName);
                cmd.Parameters.AddWithValue("@Email", users.Email);
                cmd.Parameters.AddWithValue("@PhoneNo", users.PhoneNo);
                cmd.Parameters.AddWithValue("@Password", users.Password);
                connection.Open();
                int i = cmd.ExecuteNonQuery();
                connection.Close();
                if (i > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "User registered successfully.";
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "User registration failed.";
                }
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Response login(Users users, SqlConnection connection)
        {
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter("sql_login", connection);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("@Email", users.Email);
                adapter.SelectCommand.Parameters.AddWithValue("@Password", users.Password);
                DataTable dt = new();
                adapter.Fill(dt);
                Response response = new Response();
                if (dt.Rows.Count > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "User is valid";
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "User is invalid";
                }
                return response;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
