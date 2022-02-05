using System.Data.SqlClient;
using Model;

namespace DL
{
  public class StoreFrontSQLRepository : IStoreFrontRepository
  {
    private readonly string _connectionString;
    public StoreFrontSQLRepository(string p_connectionString)
    {
      _connectionString = p_connectionString;
    }
    public StoreFront AddStoreFront(StoreFront p_storef)
    {
      string _sqlQuery = @"INSERT INTO StoreFronts 
                        VALUES(@storeID, @storeName, @storeAddress, @createdAt)";

      p_storef.StoreID = Guid.NewGuid().ToString();
      p_storef.createdAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        conn.Open();

        SqlCommand command = new SqlCommand(_sqlQuery, conn);

        command.Parameters.AddWithValue("@storeID", p_storef.StoreID);
        command.Parameters.AddWithValue("@storeName", p_storef.Name);
        command.Parameters.AddWithValue("@storeAddress", p_storef.Address);
        command.Parameters.AddWithValue("@createdAt", p_storef.createdAt);

        command.ExecuteNonQuery();
      }

      return p_storef;
    }

    public List<StoreFront> GetALlStoreFronts()
    {
      string _sqlQuery = @"SELECT * FROM StoreFronts";
      List<StoreFront> _listStoreF = new List<StoreFront>();

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        conn.Open();

        SqlCommand command = new SqlCommand(_sqlQuery, conn);

        SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
          _listStoreF.Add(new StoreFront()
          {
            StoreID = reader.GetString(0),
            Name = reader.GetString(1),
            Address = reader.GetString(2),
            createdAt = reader.GetDateTime(3)
          });
        }
      }

      return _listStoreF;
    }

    public StoreFront SaveStoreFront(StoreFront p_storef)
    {
      string _sqlQuery = @"UPDATE StoreFronts 
                        SET storeName = @storeName,
                          storeAddress = @storeAddress
                        WHERE storeID = @storeID";

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        conn.Open();

        SqlCommand command = new SqlCommand(_sqlQuery, conn);
        command.Parameters.AddWithValue("@storeName", p_storef.Name);
        command.Parameters.AddWithValue("@storeAddress", p_storef.Address);
        command.Parameters.AddWithValue("storeID", p_storef.StoreID);

        command.ExecuteNonQuery();
      }

      return p_storef;
    }
  }
}