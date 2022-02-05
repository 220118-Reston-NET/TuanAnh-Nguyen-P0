using System.Data.SqlClient;
using System.Text.Json;
using Model;

namespace DL
{
  public class CustomerSQLRepository : ICustomerRepository
  {
    private readonly string _connectionString;
    public CustomerSQLRepository(string p_connectionString)
    {
      _connectionString = p_connectionString;
    }
    public Customer AddCustomer(Customer p_cus)
    {
      string _sqlQuery = @"INSERT INTO Customers
                VALUES(@cusID, @cusName, @cusAddress, @cusEmail, @cusPhoneNo, @createdAt)";

      p_cus.CustomerID = Guid.NewGuid().ToString();
      p_cus.createdAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        conn.Open();

        SqlCommand command = new SqlCommand(_sqlQuery, conn);
        command.Parameters.AddWithValue("@cusID", p_cus.CustomerID);
        command.Parameters.AddWithValue("@cusName", p_cus.Name);
        command.Parameters.AddWithValue("@cusAddress", p_cus.Address);
        command.Parameters.AddWithValue("@cusEmail", p_cus.Email);
        command.Parameters.AddWithValue("@cusPhoneNo", p_cus.PhoneNumber);
        command.Parameters.AddWithValue("@createdAt", p_cus.createdAt);

        command.ExecuteNonQuery();
      }

      return p_cus;
    }

    public List<Customer> GetALlCustomers()
    {
      string _sqlQuery = @"SELECT * FROM Customers";
      List<Customer> _listCus = new List<Customer>();

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        conn.Open();

        SqlCommand command = new SqlCommand(_sqlQuery, conn);

        SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
          _listCus.Add(new Customer()
          {
            CustomerID = reader.GetString(0),
            Name = reader.GetString(1),
            Address = reader.GetString(2),
            Email = reader.GetString(3),
            PhoneNumber = reader.GetString(4),
            createdAt = reader.GetDateTime(5)
          });
        }
      }

      return _listCus;
    }

    public Customer SaveCustomer(Customer p_cus)
    {
      string _sqlQuery = @"UPDATE Customers
                          SET cusName = @cusName,
                            cusAddress = @cusAddress,
                            cusEmail = @cusEmail,
                            cusPhoneNo = @cusPhoneNo
                          WHERE cusID = @cusID";

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        conn.Open();

        SqlCommand command = new SqlCommand(_sqlQuery, conn);
        command.Parameters.AddWithValue("@cusName", p_cus.Name);
        command.Parameters.AddWithValue("@cusAddress", p_cus.Address);
        command.Parameters.AddWithValue("@cusEmail", p_cus.Email);
        command.Parameters.AddWithValue("@cusPhoneNo", p_cus.PhoneNumber);
        command.Parameters.AddWithValue("@cusID", p_cus.CustomerID);

        command.ExecuteNonQuery();
      }

      return p_cus;
    }
  }
}