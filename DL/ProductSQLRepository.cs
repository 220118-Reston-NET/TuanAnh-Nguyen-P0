using System.Data.SqlClient;
using Model;

namespace DL
{
  public class ProductSQLRepository : IProductRepository
  {
    private readonly string _connectionString;
    public ProductSQLRepository(string p_connectionString)
    {
      _connectionString = p_connectionString;
    }
    public Products AddProduct(Products p_prod)
    {
      string _sqlQuery = @"INSERT INTO Products
                  VALUES(@productID, @productName, @productPrice, @productDesc, @createdAt)";

      p_prod.ProductID = Guid.NewGuid().ToString();
      p_prod.createdAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        conn.Open();

        SqlCommand command = new SqlCommand(_sqlQuery, conn);
        command.Parameters.AddWithValue("@productID", p_prod.ProductID);
        command.Parameters.AddWithValue("@productName", p_prod.Name);
        command.Parameters.AddWithValue("@productPrice", p_prod.Price);
        command.Parameters.AddWithValue("@productDesc", p_prod.Desc);
        command.Parameters.AddWithValue("@createdAt", p_prod.createdAt);

        command.ExecuteNonQuery();
      }

      return p_prod;
    }

    public List<Products> GetAllProducts()
    {
      string _sqlQuery = @"SELECT * FROM Products";
      List<Products> _listProds = new List<Products>();

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        conn.Open();

        SqlCommand command = new SqlCommand(_sqlQuery, conn);

        SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
          _listProds.Add(new Products()
          {
            ProductID = reader.GetString(0),
            Name = reader.GetString(1),
            Price = reader.GetInt32(2),
            Desc = reader.GetString(3),
            createdAt = reader.GetDateTime(4)
          });
        }
      }

      return _listProds;
    }

    public Products GetProductDetailByProductId(string p_prodID)
    {
      return GetAllProducts().Where(p => p.ProductID == p_prodID).First();
    }

    public Products SaveProduct(Products p_prod)
    {
      string _sqlQuery = @"UPDATE Products 
                          SET productName = @productName,
                            productPrice = @productPrice,
                            productDesc = @productDesc'
                          WHERE productID = @productID";

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        conn.Open();

        SqlCommand command = new SqlCommand(_sqlQuery, conn);

        command.Parameters.AddWithValue("@productName", p_prod.Name);
        command.Parameters.AddWithValue("@productPrice", p_prod.Price);
        command.Parameters.AddWithValue("@productDesc", p_prod.Desc);
        command.Parameters.AddWithValue("@productID", p_prod.ProductID);

        command.ExecuteNonQuery();
      }

      return p_prod;
    }
  }
}